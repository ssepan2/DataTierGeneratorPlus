using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ssepan.Application;
using Ssepan.Application.WinForms;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary
{
    public class GeneratorController<TModel> :
        ModelController<TModel>
        where TModel :
            class,
            IModel,
            new()
    {
        #region Declarations
        //TODO:move into model/viewmodel?--SJS
        private static int percentComplete = 0;
        private static int countComplete = 0;
        private static int countTotal = 0;
        #endregion Declarations

        #region Constructors
        #endregion Constructors

        #region Properties
        #endregion Properties

        #region Methods
        #region public methods
        /// <summary>
        /// adapter for LoadTableList to be called by BackgroundWorker component.
        /// </summary>
        /// <param name="model">The container with all of the model freshly scraped from the screen.</param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <param name="errorMessage"></param>
        /// <returns name="List<Table>">The List of tables (and optional details) that will be passed back to the datagrid.</returns>
        public static List<Table> LoadTableListInBackground
        (
            GeneratorModel model,
            ref String errorMessage,
            BackgroundWorker worker = null,
            DoWorkEventArgs e = null
        )
        {
            List<Table> returnValue = default(List<Table>);
            String connectionString = default(String); ;

            try
            {
                returnValue = new List<Table>();

                GeneratorModel.DatabaseUtility = DbUtilityFactory.GetUtility(model);

                // Build the connection string
                connectionString = GeneratorModel.DatabaseUtility.GetConnectionString(model);

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    connection.Open();

                    // Get a list of the entities in the database
                    DataTable dataTable = new DataTable();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(GeneratorModel.DatabaseUtility.GetTableQuery(connection.Database), connection);
                    dataAdapter.Fill(dataTable);

                    countComplete = 0;
                    //countTotal = tableList.Count;
                    //percentComplete = 0;
                    //backgroundWorker.ReportProgress(percentComplete);

                    // Process each table
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                        }
                        else
                        {

                            Table table = new Table();

                            table.Catalog = (String)dataRow["TABLE_CATALOG"];
                            table.Schema = (String)dataRow["TABLE_SCHEMA"];
                            table.Name = (String)dataRow["TABLE_NAME"];

                            // Use the table name if an alias is not found
                            table.ProgrammaticAlias = 
                                (
                                    dataRow["ProgrammaticAlias"] == DBNull.Value 
                                    ? 
                                    table.Name 
                                    : 
                                    (String)dataRow["ProgrammaticAlias"] 
                                );

                            //default to value of AutoSelectOnLoad
                            table.IsSelected = model.AutoSelectOnLoad;

                            //moved loading of portions of dataset BELOW top-level table list to Generate(). 
                            //This will delay some (expensive) processing until we know if the user wants to generate it.--SJS
                            if (model.FetchTableDetailsWithLoad)
                            {
                                if (!QueryTable(connection, table))
                                {
                                    throw new ApplicationException("Unable to Query Table.");
                                }
                            }

                            returnValue.Add(table);

                            countComplete++;
                            //percentComplete = (countComplete / countTotal);
                            worker.ReportProgress(countComplete);
                        }
                    }

                    //percentComplete = 100;
                    //backgroundWorker.ReportProgress(percentComplete);
                }
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// adapter for Generate to be called by BackgroundWorker component.
        /// </summary>
        /// <param name="model">The container with all of the model freshly scraped from the screen.</param>
        /// <param name="tableList">
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Boolean GenerateInBackground
        (
            GeneratorModel model,
            List<Table> tableList,
            ref String errorMessage,
            BackgroundWorker worker,
            DoWorkEventArgs e
        )
        {
            Boolean returnValue = default(Boolean);

            try
            {
                if (model.GenerateStoredProcedures)
                {
                    if (!GenerateSQL(model, tableList, worker, e, ref errorMessage))
                    {
                        throw new ApplicationException("Unable to Generate SQL.");
                    }
                }

                if (model.GenerateDataLayerClasses || model.GenerateWcfLayerClasses)
                {
                    if (!GenerateDotNet(model, tableList, worker, e, ref errorMessage))
                    {
                        throw new ApplicationException("Unable to Generate DotNet.");
                    }
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Generates the SQL code for the specified database.
        /// </summary>
        /// <param name="model">The container with all of the model freshly scraped from the screen.</param>
        /// <param name="tableList"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Boolean GenerateSQL
        (
            GeneratorModel model,
            List<Table> tableList,
            BackgroundWorker worker,
            DoWorkEventArgs e,
            ref String errorMessage
        )
        {
            Boolean returnValue = default(Boolean);
            String connectionString;
            String databaseName;
            String sqlPath;
            Boolean deleteIfExists = false;

            try
            {
                // Build the connection string
                connectionString = GeneratorModel.DatabaseUtility.GetConnectionString(model);

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    // Generate the necessary SQL and .Net code for each table
                    if (tableList.Count > 0)
                    {
                        //create the appropriate dbms generator class here, 
                        IDbGenerator objDBMSGenerator = DbGeneratorFactory.GetGenerator(model);

                        //get database name that is to be used for the path of the generated code files
                        databaseName = connection.Database;

                        //determine if output goes to default location
                        if (model.OutputPath == String.Empty)
                        {
                            //code no longer goes in legacy location: (<database>\<[sql|language]> under the folder with the EXE file);
                            //it goes in <special_folder>\DataTierGeneratorPlusLibrary\<database>\<[sql|language]>
                            sqlPath = Path.Combine(Path.Combine(GeneratorModel.GetDefaultOutputPath(), databaseName), objDBMSGenerator.CodeFolder);
                            deleteIfExists = true;
                        }
                        else
                        {
                            //code goes directly in specified location, such as a project source folder
                            sqlPath = model.OutputPath;
                            deleteIfExists = false;
                        }

                        //create folder for sql code
                        Utility.CreateSubDirectory(sqlPath, deleteIfExists);

                        // Create the necessary database logins
                        if (!objDBMSGenerator.CreateUserQueries(databaseName, sqlPath, model))
                        {
                            throw new ApplicationException("Unable to Create User Queries.");
                        }

                        // Create the SQL code, per table selected
                        connection.Open();

                        countComplete = 0;
                        countTotal = tableList.Count;
                        percentComplete = 0;
                        worker.ReportProgress(percentComplete);

                        foreach (Table table in tableList)
                        {
                            if (worker.CancellationPending)
                            {
                                e.Cancel = true;
                            }
                            else
                            {

                                if (table.IsSelected)
                                {
                                    if (!model.FetchTableDetailsWithLoad)
                                    {
                                        //moved loading of portions of dataset BELOW the top-level table list to here. 
                                        //This  delays some processing until we know if the user wants to generate it.--SJS
                                        if (!QueryTable(connection, table))
                                        {
                                            throw new ApplicationException("Unable to Query Table.");
                                        }
                                    }

                                    //Generate SQL stored procedures for Data Access 
                                    if (!objDBMSGenerator.CreateStoredProcedures(table, sqlPath, model))
                                    {
                                        throw new ApplicationException("Unable to Create Stored Procedures.");
                                    }
                                }

                                countComplete++;
                                percentComplete = (countComplete / countTotal) * 100;
                                worker.ReportProgress(percentComplete);
                            }
                        }

                        percentComplete = 100;
                        worker.ReportProgress(percentComplete);
                    }

                }//end using
                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                //throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Generates the .Net code for the specified database.
        /// </summary>
        /// <param name="model">The container with all of the model freshly scraped from the screen.</param>
        /// <param name="tableList"></param>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Boolean GenerateDotNet
        (
            GeneratorModel model,
            List<Table> tableList,
            BackgroundWorker worker,
            DoWorkEventArgs e,
            ref String errorMessage
        )
        {
            Boolean returnValue = default(Boolean);
            String connectionString;
            String databaseName;
            String dotNetPath;
            Boolean deleteIfExists = false;
            StringBuilder wcfServiceConfigSnippetStringBuilder = new StringBuilder();

            try
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {

                    // Build the connection string
                    connectionString = GeneratorModel.DatabaseUtility.GetConnectionString(model);

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {

                        // Generate the necessary SQL and .Net code for each table
                        if (tableList.Count > 0)
                        {

                            //create the appropriate language generator class here, 
                            IVsGenerator objDotNetGenerator = VsGeneratorFactory.GetGenerator(model);

                            //get database name that is to be used for the path of the generated code files
                            databaseName = connection.Database;

                            //determine if output goes to default location
                            if (model.OutputPath == String.Empty)
                            {
                                //code no longer goes in legacy location: (<database>\<[sql|language]> under the folder with the EXE file);
                                //it goes in <special_folder>\DataTierGeneratorPlusLibrary\<database>\<[sql|language]>
                                dotNetPath = Path.Combine(Path.Combine(GeneratorModel.GetDefaultOutputPath(), databaseName), objDotNetGenerator.CodeFolder);
                                deleteIfExists = true;
                            }
                            else
                            {
                                //code goes directly in specified location, such as a project source folder
                                dotNetPath = model.OutputPath;
                                deleteIfExists = false;
                            }
                            
                            //create folder for .net code
                            Utility.CreateSubDirectory(dotNetPath, deleteIfExists);

                            //Generate helper classes for BLL
                            if (model.GenerateDataLayerClasses)
                            {
                                // (Optional) build the utility class (will only happen if this method is implemented)
                                if (!objDotNetGenerator.CreateDatabaseUtilityClass(dotNetPath, model))
                                {
                                    throw new ApplicationException("Unable to Create DatabaseUtility Class.");
                                }
                                // build the null handler class 
                                if (!objDotNetGenerator.CreateNullHandlerClass(dotNetPath, model))
                                {
                                    throw new ApplicationException("Unable to Create NullHandler Class.");
                                }
                            }

                            //Generate helper classes for BLL or WCF
                            if (model.GenerateDataLayerClasses || (model.GenerateWcfLayerClasses && (model.GenerateWcfLayerServerComponents || model.GenerateWcfLayerClientHelpers)))
                            {
                                // build the property comparer class 
                                if (!objDotNetGenerator.CreateSortComparerClass(dotNetPath, model))
                                {
                                    throw new ApplicationException("Unable to Create SortComparer Class.");
                                }
                                // build the Searchable Sortable BindingList class 
                                if (!objDotNetGenerator.CreateBindingListViewClass(dotNetPath, model))
                                {
                                    throw new ApplicationException("Unable to Create BindingListView Class.");
                                }
                            }

                            // Create the .Net code, per table selected
                            connection.Open();

                            countComplete = 0;
                            countTotal = tableList.Count;
                            percentComplete = 0;
                            worker.ReportProgress(percentComplete);

                            //Generate config per table for WCF
                            if (model.GenerateWcfLayerClasses && model.GenerateWcfLayerServerComponents)
                            {
                                //Generate WCF Service Config System.ServiceModel snippet; do not write until Serve and Behavior tags have been filled out. 
                                if (!objDotNetGenerator.GetWcfServiceConfigSystemServiceModelSnippet(ref wcfServiceConfigSnippetStringBuilder, model))
                                {
                                    throw new ApplicationException("Unable to Get Wcf Service Config System.ServiceModel Snippet.");
                                }
                            }

                            foreach (Table table in tableList)
                            {
                                if (worker.CancellationPending)
                                {
                                    e.Cancel = true;
                                }
                                else
                                {

                                    if (table.IsSelected)
                                    {
                                        //Do not run this again -- unless stored proceures have NOT been run; it should already have been run by GenerateSQL().
                                        //TODO: write a more reliable method of determining whether the table needs to be loaded 
                                        //when FetchTableDetailsWithLoad is false. Not critical for this implementation, 
                                        //but it would be a more reliable design.

                                        if (!model.FetchTableDetailsWithLoad && !model.GenerateStoredProcedures)
                                        {
                                            //moved loading of portions of dataset BELOW the top-level table list to here. 
                                            //This  delays some processing until we know if the user wants to generate it.--SJS
                                            if (!QueryTable(connection, table))
                                            {
                                                throw new ApplicationException("Unable to Query Table.");
                                            }
                                        }

                                        //Generate classes per table for BLL
                                        if (model.GenerateDataLayerClasses)
                                        {
                                            //Generate TableXxx class with columns as properties. 
                                            if (!objDotNetGenerator.CreateTableStructureClass(table, dotNetPath, model))
                                            {
                                                throw new ApplicationException("Unable to Create Table Structure Class.");
                                            }

                                            // Build the wrapper class(es) 
                                            if (!objDotNetGenerator.CreateDataAccessGeneratedClass(table, dotNetPath, model))
                                            {
                                                throw new ApplicationException("Unable to Create Data Access Generated Class.");
                                            }
                                            if (model.GenerateCustomClassTemplate)
                                            {
                                                if (!objDotNetGenerator.CreateDataAccessCustomClass(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Data Access Custom Class.");
                                                }
                                            }
                                        }

                                        //Generate classes per table for WCF
                                        if (model.GenerateWcfLayerClasses)
                                        {
                                            if (model.GenerateWcfLayerServerComponents)
                                            {
                                                //Generate WCF Service Config Service and Behavior snippets. 
                                                if (!objDotNetGenerator.GetWcfServiceConfigServiceAndBehaviorSnippet(ref wcfServiceConfigSnippetStringBuilder, table, model))
                                                {
                                                    throw new ApplicationException("Unable to Get Wcf Service Config Service And Behavior Snippet.");
                                                }

                                                //Generate WCF Service SVC file. 
                                                if (!objDotNetGenerator.CreateWcfServiceSVCFile(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service SVC File.");
                                                }

                                                //Generate WCF service contract class
                                                if (!objDotNetGenerator.CreateWcfContractAndInterfaceClasses(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Contract And Interface Classes.");
                                                }

                                                // Build WCF service wrapper class(es) 
                                                if (!objDotNetGenerator.CreateWcfServiceGeneratedClass(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Generated Class.");
                                                }
                                                if (model.GenerateCustomClassTemplate)
                                                {
                                                    if (!objDotNetGenerator.CreateWcfServiceCustomClass(table, dotNetPath, model))
                                                    {
                                                        throw new ApplicationException("Unable to Create Wcf Service Custom Class.");
                                                    }
                                                }

                                                // Build WCF service extension class 
                                                if (!objDotNetGenerator.CreateWcfServiceExtensionClass(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Extension Class.");
                                                }
                                            }

                                            if (model.GenerateWcfLayerClientHelpers)
                                            {
                                                // Build WCF service client extension class 
                                                if (!objDotNetGenerator.CreateWcfServiceClientExtensionClass(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Client Extension Class.");
                                                }

                                                // Build WCF service client application-code class 
                                                if (!objDotNetGenerator.CreateWcfServiceClientAppCodeClass(table, dotNetPath, model))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Client AppCode Class.");
                                                }
                                            }
                                        }

                                        countComplete++;
                                        percentComplete = (countComplete / countTotal) * 100;
                                        worker.ReportProgress(percentComplete);

                                    }

                                    percentComplete = 100;
                                    worker.ReportProgress(percentComplete);

                                }
                            }
                            
                            //Generate config per table for WCF
                            if (model.GenerateWcfLayerClasses && model.GenerateWcfLayerServerComponents)
                            {
                                //Write WCF Service Config System.ServiceModel snippet. 
                                if (!objDotNetGenerator.WriteWcfServiceConfigSystemServiceModelSnippet(ref wcfServiceConfigSnippetStringBuilder, dotNetPath, model))
                                {
                                    throw new ApplicationException("Unable to Write Wcf Service Config System.ServiceModel Snippet.");
                                }
                            }
                        }

                    }//end using
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                //throw;
            }
            return returnValue;
        }
        #endregion public methods

        #region private methods
        /// <summary>
        /// Retrieves the column, primary key, and foreign key information for the specified table.
        /// </summary>
        /// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
        /// <param name="table">The table instance that information should be retrieved for.</param>
        private static Boolean QueryTable
        (
            OleDbConnection connection,
            Table table
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Get a list of the entities in the database
                DataTable dataTable = new DataTable();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(GeneratorModel.DatabaseUtility.GetColumnQuery(table), connection);
                dataAdapter.Fill(dataTable);

                //It is possible to have Generate clicked 2x or more for each Load; 
                //Check for the existence of columns, which indicates that the details have been loaded for the current table.
                //This resolves bug that loads the columns and keys multiple times.--SJS, 11/8/2005
                if (table.Columns.Count == 0)
                {

                    // Get the list of column metadata
                    foreach (DataRow columnMetadataRow in dataTable.Rows)
                    {

                        Column column = new Column();
                        column.Name = columnMetadataRow["COLUMN_NAME"].ToString();
                        column.Type = columnMetadataRow["DATA_TYPE"].ToString();
                        column.Precision = columnMetadataRow["NUMERIC_PRECISION"].ToString();
                        column.Scale = columnMetadataRow["NUMERIC_SCALE"].ToString();

                        // Determine the column's extended name
                        column.ProgrammaticAlias = 
                            (
                                columnMetadataRow["ProgrammaticAlias"] == DBNull.Value 
                                ? 
                                column.Name 
                                : 
                                columnMetadataRow["ProgrammaticAlias"].ToString()
                            );

                        // Determine the column's length
                        column.Length = 
                            (
                                columnMetadataRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value 
                                ? 
                                columnMetadataRow["CHARACTER_MAXIMUM_LENGTH"].ToString() 
                                : 
                                columnMetadataRow["COLUMN_LENGTH"].ToString()
                            );

                        // Is the column a RowGuidCol column?
                        column.IsRowGuidCol = (columnMetadataRow["IsRowGuidCol"].ToString() == "1");

                        // Is the column an Identity column?
                        column.IsIdentity = (columnMetadataRow["IsIdentity"].ToString() == "1");

                        // Is columnRow column a computed column?
                        column.IsComputed = (columnMetadataRow["IsComputed"].ToString() == "1");

                        // Is columnRow column a nullable column?
                        column.IsNullable = (columnMetadataRow["IS_NULLABLE"].ToString() == "YES");

                        table.Columns.Add(column);
                    }

                    // Get the list of primary keys
                    DataTable primaryKeyTable = GeneratorModel.DatabaseUtility.GetPrimaryKeyList(connection, table);
                    foreach (DataRow primaryKeyRow in primaryKeyTable.Rows)
                    {
                        String primaryKeyName = primaryKeyRow["COLUMN_NAME"].ToString();

                        foreach (Column primaryKeyColumn in table.Columns)
                        {
                            if (primaryKeyColumn.Name == primaryKeyName)
                            {
                                //set column flag
                                primaryKeyColumn.IsPrimaryKey = true;
                                //add to PK list
                                table.PrimaryKeys.Add(primaryKeyColumn);
                                break;
                            }
                        }
                    }

                    // Get the list of foreign keys
                    DataTable foreignKeyTable = GeneratorModel.DatabaseUtility.GetForeignKeyList(connection, table);
                    foreach (DataRow foreignKeyRow in foreignKeyTable.Rows)
                    {
                        String fkName = foreignKeyRow["FK_NAME"].ToString();
                        String fkColumnName = foreignKeyRow["FKCOLUMN_NAME"].ToString();

                        if (table.ForeignKeys.ContainsKey(fkName) == false)
                        {
                            table.ForeignKeys.Add(fkName, new List<Column>());
                        }

                        List<Column> foreignKeys = (List<Column>)table.ForeignKeys[fkName];

                        foreach (Column foreignKeyColumn in table.Columns)
                        {
                            if (foreignKeyColumn.Name == fkColumnName)
                            {
                                foreignKeys.Add(foreignKeyColumn);
                                break;
                            }
                        }
                    }

                }
                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                //throw;
            }
            return returnValue;
        }
        #endregion private methods
        #endregion Methods
    }
}
