using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
using Ssepan.Application;
using Ssepan.Patterns;
using Ssepan.Utility;

namespace DataTierGeneratorPlus 
{
    /// <summary>
    /// Contains members needed to generate. This is the Model. It is the observable in Observer pattern. 
    /// </summary>
    public class GeneratorModel : 
        ModelBase
    {

        #region declarations
        public static IDbUtility dbUtility = null;
        
        private static int percentComplete = 0;
        private static int countComplete = 0;
        private static int countTotal = 0;
        private static BackgroundWorker backgroundWorker = null;
        private static DoWorkEventArgs doWorkEventArgs = null;
        #endregion declarations

        #region constructors
        static GeneratorModel() 
        {
            try
            {
                AsStatic = new GeneratorModel();
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw;
            }
        }

        public GeneratorModel() 
        {
        }
        #endregion constructors

        #region Properties
        public new static GeneratorModel AsStatic
        {
            get { return ModelBase.AsStatic as GeneratorModel; }
            set
            {
                ModelBase.AsStatic = value;
            }
        }
        #endregion Properties

        #region public methods
        /// <summary>
        /// adapter for LoadTableList to be called by BackgroundWorker component.
		/// </summary>
		/// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <returns name="ArrayList">The ArrayList of tables (and optional details) that will be passed back to the datagrid.</returns>
       public static ArrayList LoadTableListInBackground
        (
            Settings settings,
            BackgroundWorker worker,
            DoWorkEventArgs e 
        )
        {
            ArrayList returnValue = default(ArrayList);
            try
            {
                backgroundWorker = worker;//make accessible to Generate class;necessary for progress reporting. 
                doWorkEventArgs = e;//make accessible to Generate class;necessary for progress reporting.
                returnValue = LoadTableList(settings);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw;
            }
            finally
            {
                doWorkEventArgs = null;
                backgroundWorker = null;
            }
            return returnValue;
        }
        
        /// <summary>
		/// Generates the SQL and .Net code for the specified database.
		/// </summary>
		/// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <returns name="ArrayList">The ArrayList of tables (and optional details) that will be passed back to the datagrid.</returns>
		public static ArrayList LoadTableList
		( 
			Settings settings
		) 
		{
            ArrayList returnValue = default(ArrayList);
			String connectionString;
            try
            {
                returnValue = new ArrayList();

                dbUtility = DbUtilityFactory.GetUtility(settings);

                // Build the connection string
                connectionString = dbUtility.GetConnectionString(settings);

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    connection.Open();

                    // Get a list of the entities in the database
                    DataTable dataTable = new DataTable();
                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(GeneratorModel.dbUtility.GetTableQuery(connection.Database), connection);
                    dataAdapter.Fill(dataTable);

                    countComplete = 0;
                    //countTotal = tableList.Count;
                    //percentComplete = 0;
                    //backgroundWorker.ReportProgress(percentComplete);

                    // Process each table
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        if (backgroundWorker.CancellationPending)
                        {
                            doWorkEventArgs.Cancel = true;
                        }
                        else
                        {

                            Table table = new Table();

                            table.Catalog = (string)dataRow["TABLE_CATALOG"];
                            table.Schema = (string)dataRow["TABLE_SCHEMA"];
                            table.Name = (string)dataRow["TABLE_NAME"];

                            // Use the table name if an alias is not found
                            if (dataRow["ProgrammaticAlias"] == DBNull.Value)
                            {
                                table.ProgrammaticAlias = table.Name;
                            }
                            else
                            {
                                table.ProgrammaticAlias = (string)dataRow["ProgrammaticAlias"];
                            }

                            //default to value of AutoSelectOnLoad
                            table.IsSelected = settings.AutoSelectOnLoad;

                            //moved loading of portions of dataset BELOW top-level table list to Generate(). 
                            //This will delay some (expensive) processing until we know if the user wants to generate it.--SJS
                            if (settings.FetchTableDetailsWithLoad)
                            {
                                if (!QueryTable(connection, table))
                                {
                                    throw new ApplicationException("Unable to Query Table.");
                                }
                            }

                            returnValue.Add(table);

                            countComplete++;
                            //percentComplete = (countComplete / countTotal);
                            backgroundWorker.ReportProgress(countComplete);
                        }
                    }

                    //percentComplete = 100;
                    //backgroundWorker.ReportProgress(percentComplete);
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw;
            }
            return returnValue;
        }
        //TODO:move generate methods to controller
        //TODO:see DocumentScanner project for BackgroundWorker error message passing
        /// <summary>
        /// adapter for Generate to be called by BackgroundWorker component.
        /// </summary>
        /// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <param name="tableList">The ArrayList of tables (and optional details) that will be passed in from the datagrid.</returns>
        public static Boolean GenerateInBackground
        (
             Settings settings,
             ArrayList tableList,
             BackgroundWorker worker,
             DoWorkEventArgs e
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                backgroundWorker = worker;//make accessible to Generate class;necessary for progress reporting.
                doWorkEventArgs = e;//make accessible to Generate class;necessary for progress reporting.
                if (!Generate(settings, tableList))
                { 
                        throw new ApplicationException("Unable to Generate.");
                }
                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw;
            }
            finally
            {
                doWorkEventArgs = null;
                backgroundWorker = null;
            }
            return returnValue;
        }

		/// <summary>
		/// Generates the SQL and .Net code for the specified database.
		/// </summary>
        /// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <param name="tableList">The ArrayList of tables (and optional details) that will be passed in from the datagrid.</param>
        public static Boolean Generate
		( 
			Settings settings,
			ArrayList tableList
		) 
		{
            Boolean returnValue = default(Boolean);
            String outputPath;

            try
            {
                if (settings.GenerateStoredProcedures)
                {
                    if (!GenerateSQL(settings, tableList))
                    {
                        throw new ApplicationException("Unable to Generate SQL.");
                    }
                }

                if (settings.GenerateDataLayerClassesCheckBox || settings.GenerateWcfLayerClasses)
                {
                    if (!GenerateDotNet(settings, tableList))
                    {
                        throw new ApplicationException("Unable to Generate DotNet.");
                    }
                }

                #region open explorer
                //TODO: put this in a separate procedure 
                //determine if output goes to default location
                if (settings.OutputPath == String.Empty)
                {
                    //<special_folder>\DataTierGeneratorPlus
                    outputPath = GetDefaultOutputPath();
                }
                else
                {
                    //code goes directly in specified location, such as a project source folder
                    outputPath = settings.OutputPath;
                }

                //Use the Environment.SpecialFolder.ProgramFiles
                //to get the path to Program Files in case it
                //isn't stored on the C: drive.
                string path = outputPath;

                //Get the Windows directory
                string windir = Environment.GetEnvironmentVariable("WINDIR");
                System.Diagnostics.Process prc = new System.Diagnostics.Process();

                //Use the Windows Explorer to view the Program Files directory
                prc.StartInfo.FileName = Path.Combine(windir, @"explorer.exe");
                prc.StartInfo.Arguments = path;
                prc.Start();
                #endregion open explorer

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Generates the SQL code for the specified database.
        /// </summary>
        /// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <returns name="tableList">The ArrayList of tables (and optional details) that will be passed back to the datagrid.</returns>
        public static Boolean GenerateSQL
        (
            Settings settings,
            ArrayList tableList
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
                connectionString = dbUtility.GetConnectionString(settings);

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    // Generate the necessary SQL and .Net code for each table
                    if (tableList.Count > 0)
                    {
                        //create the appropriate dbms generator class here, 
                        IDbGenerator objDBMSGenerator = DbGeneratorFactory.GetGenerator(settings);

                        //get database name that is to be used for the path of the generated code files
                        databaseName = connection.Database;

                        //determine if output goes to default location
                        if (settings.OutputPath == String.Empty)
                        {
                            //code no longer goes in legacy location: (<database>\<[sql|language]> under the folder with the EXE file);
                            //it goes in <special_folder>\DataTierGeneratorPlus\<database>\<[sql|language]>
                            sqlPath = Path.Combine(Path.Combine(GetDefaultOutputPath(), databaseName), objDBMSGenerator.CodeFolder);
                            deleteIfExists = true;
                        }
                        else
                        {
                            //code goes directly in specified location, such as a project source folder
                            sqlPath = settings.OutputPath;
                            deleteIfExists = false;
                        }

                        //create folder for sql code
                        Utility.CreateSubDirectory(sqlPath, deleteIfExists);

                        // Create the necessary database logins
                        if (!objDBMSGenerator.CreateUserQueries(databaseName, sqlPath, settings))
                        {
                            throw new ApplicationException("Unable to Create User Queries.");
                        }

                        // Create the SQL code, per table selected
                        connection.Open();

                        countComplete = 0;
                        countTotal = tableList.Count;
                        percentComplete = 0;
                        backgroundWorker.ReportProgress(percentComplete);

                        foreach (Table table in tableList)
                        {
                            if (backgroundWorker.CancellationPending)
                            {
                                doWorkEventArgs.Cancel = true;
                            }
                            else
                            {

                                if (table.IsSelected)
                                {
                                    if (!settings.FetchTableDetailsWithLoad)
                                    {
                                        //moved loading of portions of dataset BELOW the top-level table list to here. 
                                        //This  delays some processing until we know if the user wants to generate it.--SJS
                                        if (!QueryTable(connection, table))
                                        {
                                            throw new ApplicationException("Unable to Query Table.");
                                        }
                                    }

                                    //Generate SQL stored procedures for Data Access 
                                    if (!objDBMSGenerator.CreateStoredProcedures(table, sqlPath, settings))
                                    {
                                        throw new ApplicationException("Unable to Create Stored Procedures.");
                                    }
                                }

                                countComplete++;
                                percentComplete = (countComplete / countTotal) * 100;
                                backgroundWorker.ReportProgress(percentComplete);
                            }
                        }

                        percentComplete = 100;
                        backgroundWorker.ReportProgress(percentComplete);
                    }

                }//end using
                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }


        /// <summary>
        /// Generates the .Net code for the specified database.
        /// </summary>
        /// <param name="settings">The container with all of the settings freshly scraped from the screen.</param>
        /// <returns name="tableList">The ArrayList of tables (and optional details) that will be passed back to the datagrid.</returns>
        public static Boolean GenerateDotNet
        (
            Settings settings,
            ArrayList tableList
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
                if (backgroundWorker.CancellationPending)
                {
                    doWorkEventArgs.Cancel = true;
                }
                else
                {

                    // Build the connection string
                    connectionString = dbUtility.GetConnectionString(settings);

                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {

                        // Generate the necessary SQL and .Net code for each table
                        if (tableList.Count > 0)
                        {

                            //create the appropriate language generator class here, 
                            IVsGenerator objDotNetGenerator = VsGeneratorFactory.GetGenerator(settings);

                            //get database name that is to be used for the path of the generated code files
                            databaseName = connection.Database;

                            //determine if output goes to default location
                            if (settings.OutputPath == String.Empty)
                            {
                                //code no longer goes in legacy location: (<database>\<[sql|language]> under the folder with the EXE file);
                                //it goes in <special_folder>\DataTierGeneratorPlus\<database>\<[sql|language]>
                                dotNetPath = Path.Combine(Path.Combine(GetDefaultOutputPath(), databaseName), objDotNetGenerator.CodeFolder);
                                deleteIfExists = true;
                            }
                            else
                            {
                                //code goes directly in specified location, such as a project source folder
                                dotNetPath = settings.OutputPath;
                                deleteIfExists = false;
                            }
                            
                            //create folder for .net code
                            Utility.CreateSubDirectory(dotNetPath, deleteIfExists);

                            //Generate helper classes for BLL
                            if (settings.GenerateDataLayerClassesCheckBox)
                            {
                                // (Optional) build the utility class (will only happen if this method is implemented)
                                if (!objDotNetGenerator.CreateDatabaseUtilityClass(dotNetPath, settings))
                                {
                                    throw new ApplicationException("Unable to Create DatabaseUtility Class.");
                                }
                                // build the null handler class 
                                if (!objDotNetGenerator.CreateNullHandlerClass(dotNetPath, settings))
                                {
                                    throw new ApplicationException("Unable to Create NullHandler Class.");
                                }
                            }

                            //Generate helper classes for BLL or WCF
                            if (settings.GenerateDataLayerClassesCheckBox || (settings.GenerateWcfLayerClasses && (settings.GenerateWcfLayerServerComponents || settings.GenerateWcfLayerClientHelpers)))
                            {
                                // build the property comparer class 
                                if (!objDotNetGenerator.CreateSortComparerClass(dotNetPath, settings))
                                {
                                    throw new ApplicationException("Unable to Create SortComparer Class.");
                                }
                                // build the Searchable Sortable BindingList class 
                                if (!objDotNetGenerator.CreateBindingListViewClass(dotNetPath, settings))
                                {
                                    throw new ApplicationException("Unable to Create BindingListView Class.");
                                }
                            }

                            // Create the .Net code, per table selected
                            connection.Open();

                            countComplete = 0;
                            countTotal = tableList.Count;
                            percentComplete = 0;
                            backgroundWorker.ReportProgress(percentComplete);

                            //Generate config per table for WCF
                            if (settings.GenerateWcfLayerClasses && settings.GenerateWcfLayerServerComponents)
                            {
                                //Generate WCF Service Config System.ServiceModel snippet; do not write until Serve and Behavior tags have been filled out. 
                                if (!objDotNetGenerator.GetWcfServiceConfigSystemServiceModelSnippet(ref wcfServiceConfigSnippetStringBuilder, settings))
                                {
                                    throw new ApplicationException("Unable to Get Wcf Service Config System.ServiceModel Snippet.");
                                }
                            }

                            foreach (Table table in tableList)
                            {
                                if (backgroundWorker.CancellationPending)
                                {
                                    doWorkEventArgs.Cancel = true;
                                }
                                else
                                {

                                    if (table.IsSelected)
                                    {
                                        //Do not run this again -- unless stored proceures have NOT been run; it should already have been run by GenerateSQL().
                                        //TODO: write a more reliable method of determining whether the table needs to be loaded 
                                        //when FetchTableDetailsWithLoad is false. Not critical for this implementation, 
                                        //but it would be a more reliable design.

                                        if (!settings.FetchTableDetailsWithLoad && !settings.GenerateStoredProcedures)
                                        {
                                            //moved loading of portions of dataset BELOW the top-level table list to here. 
                                            //This  delays some processing until we know if the user wants to generate it.--SJS
                                            if (!QueryTable(connection, table))
                                            {
                                                throw new ApplicationException("Unable to Query Table.");
                                            }
                                        }

                                        //Generate classes per table for BLL
                                        if (settings.GenerateDataLayerClassesCheckBox)
                                        {
                                            //Generate TableXxx class with columns as properties. 
                                            if (!objDotNetGenerator.CreateTableStructureClass(table, dotNetPath, settings))
                                            {
                                                throw new ApplicationException("Unable to Create Table Structure Class.");
                                            }

                                            // Build the wrapper class(es) 
                                            if (!objDotNetGenerator.CreateDataAccessGeneratedClass(table, dotNetPath, settings))
                                            {
                                                throw new ApplicationException("Unable to Create Data Access Generated Class.");
                                            }
                                            if (settings.GenerateCustomClassTemplate)
                                            {
                                                if (!objDotNetGenerator.CreateDataAccessCustomClass(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Data Access Custom Class.");
                                                }
                                            }
                                        }

                                        //Generate classes per table for WCF
                                        if (settings.GenerateWcfLayerClasses)
                                        {
                                            if (settings.GenerateWcfLayerServerComponents)
                                            {
                                                //Generate WCF Service Config Service and Behavior snippets. 
                                                if (!objDotNetGenerator.GetWcfServiceConfigServiceAndBehaviorSnippet(ref wcfServiceConfigSnippetStringBuilder, table, settings))
                                                {
                                                    throw new ApplicationException("Unable to Get Wcf Service Config Service And Behavior Snippet.");
                                                }

                                                //Generate WCF Service SVC file. 
                                                if (!objDotNetGenerator.CreateWcfServiceSVCFile(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service SVC File.");
                                                }

                                                //Generate WCF service contract class
                                                if (!objDotNetGenerator.CreateWcfContractAndInterfaceClasses(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Contract And Interface Classes.");
                                                }

                                                // Build WCF service wrapper class(es) 
                                                if (!objDotNetGenerator.CreateWcfServiceGeneratedClass(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Generated Class.");
                                                }
                                                if (settings.GenerateCustomClassTemplate)
                                                {
                                                    if (!objDotNetGenerator.CreateWcfServiceCustomClass(table, dotNetPath, settings))
                                                    {
                                                        throw new ApplicationException("Unable to Create Wcf Service Custom Class.");
                                                    }
                                                }

                                                // Build WCF service extension class 
                                                if (!objDotNetGenerator.CreateWcfServiceExtensionClass(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Extension Class.");
                                                }
                                            }

                                            if (settings.GenerateWcfLayerClientHelpers)
                                            {
                                                // Build WCF service client extension class 
                                                if (!objDotNetGenerator.CreateWcfServiceClientExtensionClass(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Client Extension Class.");
                                                }

                                                // Build WCF service client application-code class 
                                                if (!objDotNetGenerator.CreateWcfServiceClientAppCodeClass(table, dotNetPath, settings))
                                                {
                                                    throw new ApplicationException("Unable to Create Wcf Service Client AppCode Class.");
                                                }
                                            }
                                        }

                                        countComplete++;
                                        percentComplete = (countComplete / countTotal) * 100;
                                        backgroundWorker.ReportProgress(percentComplete);

                                    }

                                    percentComplete = 100;
                                    backgroundWorker.ReportProgress(percentComplete);

                                }
                            }
                            
                            //Generate config per table for WCF
                            if (settings.GenerateWcfLayerClasses && settings.GenerateWcfLayerServerComponents)
                            {
                                //Write WCF Service Config System.ServiceModel snippet. 
                                if (!objDotNetGenerator.WriteWcfServiceConfigSystemServiceModelSnippet(ref wcfServiceConfigSnippetStringBuilder, dotNetPath, settings))
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
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
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
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(GeneratorModel.dbUtility.GetColumnQuery(table), connection);
                dataAdapter.Fill(dataTable);

                //It is possible to have Generate clicked 2x or more for each Load; 
                //Check for the existence of columns, which indicates that the details have been loaded for the current table.
                //This resolves bug that loads the columns and keys multiple times.--SJS, 11/8/2005
                if (table.Columns.Count == 0)
                {

                    foreach (DataRow columnRow in dataTable.Rows)
                    {

                        Column column = new Column();
                        column.Name = columnRow["COLUMN_NAME"].ToString();
                        column.Type = columnRow["DATA_TYPE"].ToString();
                        column.Precision = columnRow["NUMERIC_PRECISION"].ToString();
                        column.Scale = columnRow["NUMERIC_SCALE"].ToString();

                        // Determine the column's extended name
                        if (columnRow["ProgrammaticAlias"] == DBNull.Value)
                        {
                            column.ProgrammaticAlias = column.Name;
                        }
                        else
                        {
                            column.ProgrammaticAlias = columnRow["ProgrammaticAlias"].ToString();
                        }

                        // Determine the column's length
                        if (columnRow["CHARACTER_MAXIMUM_LENGTH"] != DBNull.Value)
                        {
                            column.Length = columnRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
                        }
                        else
                        {
                            column.Length = columnRow["COLUMN_LENGTH"].ToString();
                        }

                        // Is the column a RowGuidCol column?
                        if (columnRow["IsRowGuidCol"].ToString() == "1")
                        {
                            column.IsRowGuidCol = true;
                        }

                        // Is the column an Identity column?
                        if (columnRow["IsIdentity"].ToString() == "1")
                        {
                            column.IsIdentity = true;
                        }

                        // Is columnRow column a computed column?
                        if (columnRow["IsComputed"].ToString() == "1")
                        {
                            column.IsComputed = true;
                        }

                        // Is columnRow column a nullable column?
                        if (columnRow["IS_NULLABLE"].ToString() == "YES")
                        {
                            column.IsNullable = true;
                        }
                        else
                        {
                            column.IsNullable = false;
                        }

                        table.Columns.Add(column);
                    }

                    // Get the list of primary keys
                    DataTable primaryKeyTable = GeneratorModel.dbUtility.GetPrimaryKeyList(connection, table);
                    foreach (DataRow primaryKeyRow in primaryKeyTable.Rows)
                    {
                        String primaryKeyName = primaryKeyRow["COLUMN_NAME"].ToString();

                        foreach (Column pkColumn in table.Columns)
                        {
                            if (pkColumn.Name == primaryKeyName)
                            {
                                //add to PK list
                                table.PrimaryKeys.Add(pkColumn);
                                //set column flag
                                pkColumn.IsPrimaryKey = true;
                                break;
                            }
                        }
                    }

                    // Get the list of foreign keys
                    DataTable foreignKeyTable = GeneratorModel.dbUtility.GetForeignKeyList(connection, table);
                    foreach (DataRow foreignKeyRow in foreignKeyTable.Rows)
                    {
                        String fkName = foreignKeyRow["FK_NAME"].ToString();
                        String fkColumnName = foreignKeyRow["FKCOLUMN_NAME"].ToString();

                        if (table.ForeignKeys.ContainsKey(fkName) == false)
                        {
                            table.ForeignKeys.Add(fkName, new ArrayList());
                        }

                        ArrayList foreignKeys = (ArrayList)table.ForeignKeys[fkName];

                        foreach (Column fkColumn in table.Columns)
                        {
                            if (fkColumn.Name == fkColumnName)
                            {
                                foreignKeys.Add(fkColumn);
                                break;
                            }
                        }
                    }

                }
                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

        /// <summary>
        /// Get a default base output path.
        /// </summary>
        /// <returns></returns>
        
        private static String GetDefaultOutputPath()
        {
            String returnValue = default(String);
            String applicationName = String.Empty;
            try
            {
                applicationName = Path.GetFileNameWithoutExtension(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name);
                returnValue = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), applicationName);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
            }
            return returnValue;
        }
        #endregion private methods

    }
}
