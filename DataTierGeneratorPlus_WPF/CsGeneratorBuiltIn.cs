using System;
using System.Collections;
using System.IO;
using System.Text;
using Ssepan.Utility;

namespace DataTierGeneratorPlus 
{
    class CsGeneratorBuiltIn : VsGenerator
	{
		private const String CODE_FOLDER = "CS";
		private const String CODE_FILE_EXT = "cs";
        private const String DOTNET_LANGUAGE_ABBREVIATION = "C#";
		private const String TEMPLATE_FILE_VS_PRE = "Cs";

        private String _codeFolder = CODE_FOLDER;
        public override String CodeFolder
        {
            get { return _codeFolder; }
        }

		public CsGeneratorBuiltIn() {}

        #region Database Utility Class
        /// <summary>
        /// Creates a utility for opening and accessing the database.
        /// </summary>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateDatabaseUtilityClass
        (
            String path,
            Settings settings
        ) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "DatabaseUtility." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetDatabaseUtilityClass(settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the DatabaseUtility class.
        /// </summary>
        /// <param name="settings">User settings.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        protected override String GetDatabaseUtilityClass
        (
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "DatabaseUtility.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                    returnValue = returnValue.Replace("#Namespace#", settings.Namespace + ".");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                    returnValue = returnValue.Replace("#Namespace#", "");
                }
                returnValue = returnValue.Replace("#Client#", GeneratorModel.dbUtility.DotNetFrameworkClientLib());
                returnValue = returnValue.Replace("#SqlConnection#", GeneratorModel.dbUtility.DotNetFrameworkClientLibConnection());
                returnValue = returnValue.Replace("#Command#", GeneratorModel.dbUtility.DotNetFrameworkClientLibCommand());
                returnValue = returnValue.Replace("#DataAdapter#", GeneratorModel.dbUtility.DotNetFrameworkClientLibDataAdapter());
                returnValue = returnValue.Replace("#DataReader#", GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader());
                returnValue = returnValue.Replace("#Connection#", GeneratorModel.dbUtility.DotNetFrameworkClientLibConnection());
                returnValue = returnValue.Replace("#Transaction#", GeneratorModel.dbUtility.DotNetFrameworkClientLibTransaction());
                returnValue = returnValue.Replace("#Parameter#", GeneratorModel.dbUtility.DotNetFrameworkClientLibParameter());
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Database Utility Class

        #region Null Handler Class
        /// <summary>
        /// Creates a class for handling null field values.
        /// </summary>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateNullHandlerClass
        (
            String path,
            Settings settings
        ) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "NullHandler." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetNullHandlerClass(settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the NullHandler class.
        /// </summary>
        /// <param name="settings">User settings.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        protected override String GetNullHandlerClass
        (
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "NullHandler.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Null Handler Class

        #region Sort Comparer Class
        /// <summary>
        /// Creates a class to handle Sort comparisons.
        /// </summary>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateSortComparerClass
        (
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "SortComparer." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetSortComparerClass(settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the SortComparer class.
        /// </summary>
        /// <param name="settings">User settings.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        protected override String GetSortComparerClass
        (
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "SortComparer.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Sort Comparer Class

        #region BindingListView Class
        /// <summary>
        /// Creates a generic binding list view class.
        /// </summary>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateBindingListViewClass
        (
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, "BindingListView." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetBindingListViewClass(settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the BindingListView class.
        /// </summary>
        /// <param name="settings">User settings.</param>
        /// <returns>The query that should be used for retrieving column information for the specified table.</returns>
        protected override String GetBindingListViewClass
        (
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "BindingListView.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                }
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion BindingListView Class

        #region Info Class
        /// <summary>
        /// Creates a C# table structure class for all of the table's fields .
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateTableStructureClass
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Collections;");
                streamWriter.WriteLine("using System.ComponentModel;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " {");
                }

                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t/// <summary>");
                streamWriter.WriteLine("\t/// Class that stores table fields.");
                streamWriter.WriteLine("\t/// </summary>");
                streamWriter.WriteLine("\tpublic class " + className + " {");

                //do default constructor
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Default constructor.  ");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + className + "() {}");

                //begin constructor with values
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Constructor with values.  ");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + className + "(");

                // Append the method formal parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t" + CreateMethodFormalParameter(column) + ",\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));

                streamWriter.WriteLine("\t\t)");
                streamWriter.WriteLine("\t\t{");

                // Append the private field assignment statements
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        streamWriter.WriteLine("\t\t\t_" + column.ProgrammaticAlias + " = " + column.ProgrammaticAlias + ";");
                    }
                }

                streamWriter.WriteLine("\t\t}");
                //end constructor with values

                // Create a private variable for each of the columns
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// Private variables for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\tprivate " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + " _" + column.ProgrammaticAlias + ";");
                }

                // Create a property accessor for each of the columns
                Boolean IsPrimaryKey = false;
                Boolean IsIdentity = false;
                Boolean IsNullable = false;

                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// Public properties for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];

                    IsPrimaryKey = column.IsPrimaryKey;
                    IsIdentity = (column.IsIdentity || column.IsRowGuidCol);
                    IsNullable = column.IsNullable;

                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// " + column.ProgrammaticAlias + "  ");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectFieldAttribute(" + Utility.FormatCamel(IsPrimaryKey.ToString()) + ", " + Utility.FormatCamel(IsIdentity.ToString()) + ", " + Utility.FormatCamel(IsNullable.ToString()) + ")]");
                    streamWriter.WriteLine("\t\tpublic " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + " " + column.ProgrammaticAlias + "{");
                    streamWriter.WriteLine("\t\t\tget { return _" + column.ProgrammaticAlias + "; }");
                    streamWriter.WriteLine("\t\t\tset { _" + column.ProgrammaticAlias + " = value; }");
                    streamWriter.WriteLine("\t\t}");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }
        #endregion Info Class

        #region Controller Generated Class
        /// <summary>
		/// Creates a C# data access  class for all of the table's stored procedures generated automatically.
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
		/// <param name="path">Path where the class should be created.</param>
		/// <param name="settings">User settings.</param>
        public override Boolean CreateDataAccessGeneratedClass
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Controller" + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + ".Generated." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Collections.Generic;");
                streamWriter.WriteLine("using System.Data;");
                streamWriter.WriteLine("using System.Data." + GeneratorModel.dbUtility.DotNetFrameworkClientLib() + ";");
                streamWriter.WriteLine("using System.ComponentModel;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " \r\n{");
                }

                streamWriter.WriteLine("\t [DataObject(true)]");
                streamWriter.WriteLine("\tpublic partial class " + className + " \r\n\t{");
                streamWriter.WriteLine("\t\tprotected " + className + "() {}");

                // Create an enum for accessing each of the columns using an integer value, which increases performance
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t//Public enums for column positions on the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\tpublic enum ColumnIndex \r\n\t\t{");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\t\t" + Utility.FormatPascal(column.ProgrammaticAlias) + " = " + i.ToString() + ",");
                }
                streamWriter.WriteLine("\t\t};");

                // Append the access methods
                if (!CreateInsertMethod(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Method.");
                }
                if (!CreateInsertMethodTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Method Taking Info.");
                }
                if (!CreateUpdateMethod(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Method.");
                }
                if (!CreateUpdateMethodTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Method Taking Info.");
                }
                if (!CreateDeleteMethod(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Method.");
                }
                if (!CreateDeleteMethodTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Method Taking Info.");
                }
                if (!CreateDeleteByMethods(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Methods.");
                }
                if (!CreateDeleteByMethodsTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Methods Taking Info.");
                }
                if (!CreateSelectMethodReturningDataReader(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Method Returning DataReader.");
                }
                if (!CreateSelectMethodReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Method Returning DataSet.");
                }
                if (!CreateSelectMethodReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Method Returning Info.");
                }
                if (!CreateSelectAllMethodReturningDataReader(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Method Returning DataReader.");
                }
                if (!CreateSelectAllMethodReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Method Returning DataSet.");
                }
                if (!CreateSelectAllMethodReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Method Returning Info.");
                }
                if (!CreateSelectByMethodsReturningDataReader(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Methods Returning DataReader.");
                }
                if (!CreateSelectByMethodsReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Methods Returning DataSet.");
                }
                if (!CreateSelectByMethodsReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Methods Returning Info.");
                }
                if (!CreateListLoadMethod(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create List Load Method.");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the insert functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
        protected override Boolean CreateInsertMethod
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tpublic static int Insert(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tpublic static Guid Insert(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tpublic static void Insert(");
                }

                // Append the method call parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                }
                streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                // Append the connection String parameter
                streamWriter.WriteLine(") \r\n\t\t{");

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    //if (!executeScalar)
                    //{
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn (int) DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn (Guid) DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                    //}
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                }

                // Append the parameters
                builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                streamWriter.WriteLine("\t\t\t);");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                returnValue = true;
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

        /// <summary>
        /// Creates a String that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateInsertMethodTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tpublic static int InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tpublic static Guid InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tpublic static void InsertInfo(");
                }

                // Append the info parameter
                streamWriter.WriteLine("" + className + " info) \r\n\t\t{");

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    //if (!executeScalar)
                    //{
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn (int) DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn (Guid) DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                    //}
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\",");
                }

                // Append the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ",\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                streamWriter.WriteLine("\t\t\t);");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                returnValue = true;
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

		/// <summary>
		/// Creates a String that represents the update functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
        protected override Boolean CreateUpdateMethod
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]");

                    streamWriter.Write("\t\tpublic static void Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Update]\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");


                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the update functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateUpdateMethodTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]");

                    streamWriter.WriteLine("\t\tpublic static void UpdateInfo(" + className + " info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Update]\",");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");


                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the delete functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateDeleteMethod
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.Write("\t\tpublic static void Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Delete]\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the delete functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteMethodTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                    className = Utility.FilterPathCharacters(className);

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]");

                    streamWriter.WriteLine("\t\tpublic static void DeleteInfo(" + className + " info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Delete]\",");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the "delete by" functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteByMethods
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the stored procedure name
                    StringBuilder stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("[" + table.Schema + "].[" + /*settings.SPPrefix +*/ table.Name + "DeleteBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.Name));
                        }
                    }
                    stringBuilder.Append("]");
                    String procedureName = stringBuilder.ToString();

                    // Create the method name
                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.Write("\t\tpublic static void " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "delete by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteByMethodsTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the stored procedure name
                    StringBuilder stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("[" + table.Schema + "].[" + /*settings.SPPrefix +*/ table.Name + "DeleteBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.Name));
                        }
                    }
                    stringBuilder.Append("]");
                    String procedureName = stringBuilder.ToString();

                    // Create the method name
                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteInfoBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.WriteLine("\t\tpublic static void " + methodName + "(" + className + " info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\",");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the select by primary key functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectMethodReturningDataReader
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic static " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " SelectDR(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Select]\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select by primary key functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectMethodReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic static DataSet SelectDS(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Select]\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select by primary key functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectMethodReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.WriteLine("\t\tpublic static BindingListView<" + className + "> SelectInfo(" + className + " info) \r\n\t\t{");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tusing (" + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " dr = SelectDR(");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("info." + column.ProgrammaticAlias + ",");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 1));
                    streamWriter.WriteLine(")) \r\n\t\t\t{");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\treturn LoadListDR(dr);");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\t}");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the select functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectAllMethodReturningDataReader
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.WriteLine("\t\tpublic static " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " SelectDRAll() \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "SelectAll]\");");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectAllMethodReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.WriteLine("\t\tpublic static DataSet SelectDSAll() \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "SelectAll]\");");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectAllMethodReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.Write("\t\tpublic static BindingListView<" + className + "> SelectInfoAll(");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tusing (" + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " dr = SelectDRAll(");

                    // Append the parameters
                    streamWriter.WriteLine(")) \r\n\t\t\t{");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\treturn LoadListDR(dr);");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\t}");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

		/// <summary>
		/// Creates a String that represents the "select by" functionality of the data access class.
		/// </summary>
		/// <param name="table">The Table instance that this method will be created for.</param>
		/// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
		/// <param name="settings">User settings.</param>C:\Documents and Settings\ssepan\My Documents\Visual Studio 2008\Projects\DataTierGeneratorPlus6x\frmAbout.cs
        protected override Boolean CreateSelectByMethodsReturningDataReader
		(
			Table table, 
			StreamWriter streamWriter,
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the stored procedure name
                    StringBuilder stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("[" + table.Schema + "].[" + /*settings.SPPrefix +*/ table.Name + "SelectBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.Name));
                        }
                    }
                    stringBuilder.Append("]");
                    String procedureName = stringBuilder.ToString();

                    // Create the method name
                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("SelectDRBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic static " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectByMethodsReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the stored procedure name
                    StringBuilder stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("[" + table.Schema + "].[" + /*settings.SPPrefix +*/ table.Name + "SelectBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.Name));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.Name));
                        }
                    }
                    stringBuilder.Append("]");
                    String procedureName = stringBuilder.ToString();

                    // Create the method name
                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("SelectDSBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic static DataSet " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\",");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectByMethodsReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);
                    // Create the stored procedure name
                    StringBuilder stringBuilder2 = new StringBuilder(255);

                    stringBuilder.Append("SelectInfoBy");
                    stringBuilder2.Append("SelectDRBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                            stringBuilder2.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                            stringBuilder2.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();
                    String procedureName = stringBuilder2.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.WriteLine("\t\tpublic static BindingListView<" + className + "> " + methodName + "(" + className + " info) \r\n\t\t{");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tusing (" + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " dr = " + procedureName + "(");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("info." + Utility.FormatCamel(column.ProgrammaticAlias) + ",");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 1));
                    streamWriter.WriteLine(")) \r\n\t\t\t{");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\treturn LoadListDR(dr);");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\t}");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a method to load the tables fields from a DataReader into an Info object.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateListLoadMethod
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Info"));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Loads all records from the [" + table.Schema + "].[" + table.Name + "] table into List of " + className + ".");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\tprotected static BindingListView<" + className + "> LoadListDR(" + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " dr) \r\n\t\t{ ");

                    streamWriter.WriteLine("\t\t\tBindingListView<" + className + "> infoList = new BindingListView<" + className + ">();");
                    streamWriter.WriteLine("\t\t\twhile (dr.Read()) \r\n\t\t\t{");

                    streamWriter.WriteLine("\t\t\t\tinfoList.Add");

                    streamWriter.WriteLine("\t\t\t\t\t(");
                    streamWriter.WriteLine("\t\t\t\t\tnew " + className + "");

                    streamWriter.WriteLine("\t\t\t\t\t\t(");

                    // Append the method formal parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        if (column.IsIdentity == false && column.IsRowGuidCol == false)
                        {
                            builder.Append("\t\t\t\t\t\t(" + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + ")NullHandler.HandleDbNull(dr[\"" + Utility.FormatCamel(column.ProgrammaticAlias) + "\"]),\r\n");
                        }
                    }
                    builder.Remove(builder.Length - 3, 1);
                    streamWriter.Write(builder.ToString(0, builder.Length));//removes comma, but leaves newline

                    streamWriter.WriteLine("\t\t\t\t\t\t)");

                    streamWriter.WriteLine("\t\t\t\t\t);");

                    streamWriter.WriteLine("\t\t\t}");
                    streamWriter.WriteLine("\t\t\treturn infoList;");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }
        #endregion Controller Generated Class
        
        #region Controller Custom Class
        /// <summary>
        /// Creates an empty C# data access  class for all of the table's stored procedures created by hand.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateDataAccessCustomClass
       (
           Table table,
           String path,
           Settings settings
       )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Controller" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, className + ".Custom." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetDataAccessCustomClass(table, settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the Data Access Custom class.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="settings">User settings.</param>
        /// <returns>A formatted app / web config snippet.</returns>
        protected override String GetDataAccessCustomClass
        (
            Table table,
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Controller" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "DataControllerCustom.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                    returnValue = returnValue.Replace("#Namespace#", settings.Namespace + ".");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                    returnValue = returnValue.Replace("#Namespace#", "");
                }
                returnValue = returnValue.Replace("#Class#", className);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Controller Custom Class

        #region WCF Service Config Snippet
        //moved to base class, called from GeneratorModel
        #endregion WCF Service Config Snippet

        #region WCF Service SVC

        /// <summary>
        /// Returns the contents of the System ServiceModel Config snippet.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="settings">User settings.</param>
        /// <returns>A formatted app / web config snippet.</returns>
        protected override String GetWcfServiceSVCFile
        (
            Table table,
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates.WcfServiceSVC.txt");

                //if (IsNameSpace)
                //{
                returnValue = returnValue.Replace("#Namespace#", settings.Namespace + ".");
                //}
                //else
                //{
                //    returnValue = returnValue.Replace("#Namespace#", "");
                //}
                returnValue = returnValue.Replace("#Service#", className);
                returnValue = returnValue.Replace("#Language#", DOTNET_LANGUAGE_ABBREVIATION);
                returnValue = returnValue.Replace("#Extension#", CODE_FILE_EXT);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
            return returnValue;
        }
        #endregion WCF Service SVC

        #region Contract Class
        /// <summary>
        /// Creates a C# contract class for all of the table's fields .
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateWcfContractAndInterfaceClasses
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");
                
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "Contract." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Data;");
                streamWriter.WriteLine("using System.Runtime.Serialization;");
                streamWriter.WriteLine("using System.ServiceModel;");
                streamWriter.WriteLine("//using System.ComponentModel;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " \r\n{");
                }

                #region Interface
                //start interface
                streamWriter.WriteLine("\t/// <summary>");
                streamWriter.WriteLine("\t/// Class that defines the table operations.");
                streamWriter.WriteLine("\t/// </summary>");
                streamWriter.WriteLine("\t[ServiceContract]");
                streamWriter.WriteLine("\tpublic interface I" + className + "Service \r\n\t{");

                // Append the access methods interface
                if (!CreateInsertServiceInterface(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Service Interface.");
                }
                if (!CreateInsertServiceInterfaceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Service Interface Taking Info.");
                }
                if (!CreateUpdateServiceInterface(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Service Interface.");
                }
                if (!CreateUpdateServiceInterfaceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Service Interface Taking Info.");
                }
                if (!CreateDeleteServiceInterface(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Service Interface.");
                }
                if (!CreateDeleteServiceInterfaceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Service Interface Taking Info.");
                }
                if (!CreateDeleteByServiceInterfaces(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Service Interfaces.");
                }
                if (!CreateDeleteByServiceInterfacesTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Service Interfaces Taking Info.");
                }
                if (!CreateSelectServiceInterfaceReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Service Interface Returning DataSet.");
                }
                if (!CreateSelectServiceInterfaceReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Service Interface Returning Info.");
                }
                if (!CreateSelectAllServiceInterfaceReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Service Interface Returning DataSet.");
                }
                if (!CreateSelectAllServiceInterfaceReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Service Interface Returning Info.");
                }
                if (!CreateSelectByServiceInterfacesReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Service Interfaces Returning DataSet.");
                }
                if (!CreateSelectByServiceInterfacesReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Service Interfaces Returning Info.");
                }

                // Close out the interface 
                streamWriter.WriteLine("\t}");
                #endregion Interface
                
                #region Contract
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t/// <summary>");
                streamWriter.WriteLine("\t/// Class that stores table fields.");
                streamWriter.WriteLine("\t/// </summary>");
                streamWriter.WriteLine("\t[DataContract]");
                streamWriter.WriteLine("\tpublic class " + className + "Contract \r\n\t{");

                //do default constructor
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Default constructor.  ");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + className + "Contract() {}");

                //begin constructor with values
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Constructor with values.  ");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\tpublic " + className + "Contract(");

                // Append the method formal parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t" + CreateMethodFormalParameter(column) + ",\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));

                streamWriter.WriteLine("\t\t)");
                streamWriter.WriteLine("\t\t{");

                // Append the private field assignment statements
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        streamWriter.WriteLine("\t\t\t_" + column.ProgrammaticAlias + " = " + column.ProgrammaticAlias + ";");
                    }
                }

                streamWriter.WriteLine("\t\t}");
                //end constructor with values

                // Create a private variable for each of the columns
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// Private variables for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\tprivate " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + " _" + column.ProgrammaticAlias + ";");
                }

                // Create a property accessor for each of the columns
                Boolean IsPrimaryKey = false;
                Boolean IsIdentity = false;
                Boolean IsNullable = false;

                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// Public properties for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];

                    IsPrimaryKey = column.IsPrimaryKey;
                    IsIdentity = (column.IsIdentity || column.IsRowGuidCol);
                    IsNullable = column.IsNullable;

                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// " + column.ProgrammaticAlias + "  ");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[DataMember]");
                    streamWriter.WriteLine("\t\t//[DataObjectFieldAttribute(" + Utility.FormatCamel(IsPrimaryKey.ToString()) + ", " + Utility.FormatCamel(IsIdentity.ToString()) + ", " + Utility.FormatCamel(IsNullable.ToString()) + ")]");
                    streamWriter.WriteLine("\t\tpublic " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + " " + column.ProgrammaticAlias + "\r\n\t\t{");
                    streamWriter.WriteLine("\t\t\tget { return _" + column.ProgrammaticAlias + "; }");
                    streamWriter.WriteLine("\t\t\tset { _" + column.ProgrammaticAlias + " = value; }");
                    streamWriter.WriteLine("\t\t}");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");
                #endregion Contract

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateInsertServiceInterface
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t[OperationContract]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tint Insert(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tGuid Insert(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tvoid Insert(");
                }

                // Append the method call parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                }
                streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                streamWriter.WriteLine(");");

                returnValue = true;
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

        /// <summary>
        /// Creates a String that represents the insert functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateInsertServiceInterfaceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t[OperationContract]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tint InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tGuid InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tvoid InsertInfo(");
                }

                // Append the info parameter
                streamWriter.WriteLine("" + className + "Contract info);");
                returnValue = true;
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

        /// <summary>
        /// Creates a String that represents the update functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateUpdateServiceInterface
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tvoid Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the update functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateUpdateServiceInterfaceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tvoid UpdateInfo(" + className + "Contract info);");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the delete functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteServiceInterface
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tvoid Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the delete functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteServiceInterfaceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tvoid DeleteInfo(" + className + "Contract info);");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "delete by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteByServiceInterfaces
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    StringBuilder stringBuilder = new StringBuilder(255);

                    // Create the method name
                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tvoid " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "delete by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateDeleteByServiceInterfacesTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteInfoBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tvoid " + methodName + "(" + className + "Contract info);");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select by primary key functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectServiceInterfaceReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tDataSet SelectDS(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select by primary key functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectServiceInterfaceReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tBindingListView<" + className + "Contract> SelectInfo(" + className + "Contract info);");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectAllServiceInterfaceReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tDataSet SelectDSAll();");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the select functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectAllServiceInterfaceReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tBindingListView<" + className + "Contract> SelectInfoAll(");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectByServiceInterfacesReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("SelectDSBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.Write("\t\tDataSet " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(");");
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
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String that represents the "select by" functionality of the data access class.
        /// </summary>
        /// <param name="table">The Table instance that this method will be created for.</param>
        /// <param name="streamWriter">The StreamWriter instance that will be used to create the method.</param>
        /// <param name="settings">User settings.</param>
        protected override Boolean CreateSelectByServiceInterfacesReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder.Append("SelectInfoBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t[OperationContract]");

                    streamWriter.WriteLine("\t\tBindingListView<" + className + "Contract> " + methodName + "(" + className + "Contract info);");
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
            }
            return returnValue;
        }
        #endregion Contract Class

        #region Service Generated Class

        /// <summary>
        /// Creates a C# language WCF Service class for all of the table's stored procedures generated automatically.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateWcfServiceGeneratedClass
        (
           Table table,
           String path,
           Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + ".Generated." + SVC_FILE_EXT + "." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System;");
                streamWriter.WriteLine("using System.Data;");
                streamWriter.WriteLine("using System.Runtime.Serialization;");
                streamWriter.WriteLine("using System.ServiceModel;");
                streamWriter.WriteLine("//using System.ComponentModel;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " \r\n{");
                }

                #region Service Class
                //start class
                streamWriter.WriteLine("\t//[DataObject(true)]");
                streamWriter.WriteLine("\tpublic partial class " + className + " : I" + className + "\r\n\t{");
                streamWriter.WriteLine("\t\tprotected " + className + "() {}");

                // Append the access methods
                if (!CreateInsertService(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Service.");
                }
                if (!CreateInsertServiceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Service Taking Info.");
                }
                if (!CreateUpdateService(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Service.");
                }
                if (!CreateUpdateServiceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Update Service Taking Info.");
                }
                if (!CreateDeleteService(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Service.");
                }
                if (!CreateDeleteServiceTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Service Taking Info.");
                }
                if (!CreateDeleteByServices(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Services.");
                }
                if (!CreateDeleteByServicesTakingInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Services Taking Info.");
                }
                if (!CreateSelectServiceReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Service Returning DataSet.");
                }
                if (!CreateSelectServiceReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create Select Service Returning Info.");
                }
                if (!CreateSelectAllServiceReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Service Returning DataSet.");
                }
                if (!CreateSelectAllServiceReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Service Returning Info.");
                }
                if (!CreateSelectByServicesReturningDataSet(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Services Returning DataSet.");
                }
                if (!CreateSelectByServicesReturningInfo(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Services Returning Info.");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");
                #endregion Service Class

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }

        protected override Boolean CreateInsertService
        (
            Table table, 
            StreamWriter streamWriter, 
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String controllerClassName = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Controller"));
                controllerClassName = Utility.FilterPathCharacters(controllerClassName);

                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Insert, false)]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tpublic int Insert(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tpublic Guid Insert(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tpublic void Insert(");
                }

                // Append the method call parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                }
                streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                // Append the connection String parameter
                streamWriter.WriteLine(") \r\n\t\t{");

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    //if (!executeScalar)
                    //{
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn " + controllerClassName + ".Insert \r\n\t\t\t(");
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn " + controllerClassName + ".Insert \r\n\t\t\t(");
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                    //}
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\t" + controllerClassName + ".Insert \r\n\t\t\t(");
                }

                // Append the parameters
                builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                streamWriter.WriteLine("\t\t\t);");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                returnValue = true;
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

        protected override Boolean CreateInsertServiceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t/// <summary>");
                streamWriter.WriteLine("\t\t/// Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t/// </summary>");
                streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tpublic int InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tpublic Guid InsertInfo(");
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tpublic void InsertInfo(");
                }

                // Append the info parameter
                streamWriter.WriteLine("" + className + "Contract info) \r\n\t\t{");

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    //if (!executeScalar)
                    //{
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.InsertInfo\r\n\t\t\t(");
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.InsertInfo\r\n\t\t\t(");
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                    //}
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.InsertInfo\r\n\t\t\t(");
                }

                // Append the parameters
                streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info()");
                
                streamWriter.WriteLine("\t\t\t);");

                // Append the method footer
                streamWriter.WriteLine("\t\t}");

                returnValue = true;
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

        protected override Boolean CreateUpdateService
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Update, false)]");

                    streamWriter.Write("\t\tpublic void Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.Update\r\n\t\t\t(");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");


                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateUpdateServiceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Update, true)]");

                    streamWriter.WriteLine("\t\tpublic void UpdateInfo(" + className + "Contract info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.UpdateInfo\r\n\t\t\t(");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info()");

                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateDeleteService
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.Write("\t\tpublic void Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.Delete\r\n\t\t\t(");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateDeleteServiceTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]");

                    streamWriter.WriteLine("\t\tpublic void DeleteInfo(" + className + "Contract info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.DeleteInfo\r\n\t\t\t(");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info()");

                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateDeleteByServices
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.Write("\t\tpublic void " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller." + methodName + "\r\n\t\t\t(");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateDeleteByServicesTakingInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("DeleteInfoBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the delete function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Delete, false)]");

                    streamWriter.WriteLine("\t\tpublic void " + methodName + "(" + className + "Contract info) \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller." + methodName + "\r\n\t\t\t(");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info()");
                    
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectServiceReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic DataSet SelectDS(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn " + className + "Controller.SelectDS\r\n\t\t\t(");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectServiceReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.WriteLine("\t\tpublic BindingListView<" + className + "Contract> SelectInfo(" + className + "Contract info) \r\n\t\t{");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\treturn " + className + "Controller.SelectInfo(info.To" + className + "Info()).ToBindingListViewOfContract();");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectAllServiceReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.WriteLine("\t\tpublic DataSet SelectDSAll() \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn " + className + "Controller.SelectDSAll();");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectAllServiceReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.Write("\t\tpublic BindingListView<" + className + "Contract> SelectInfoAll(");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\treturn " + className + "Controller.SelectInfoAll().ToBindingListViewOfContract();");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectByServicesReturningDataSet
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder = new StringBuilder(255);
                    stringBuilder.Append("SelectDSBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, false)]");

                    streamWriter.Write("\t\tpublic DataSet " + methodName + "(");
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") \r\n\t\t{");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn " + className + "Controller." + methodName + "\r\n\t\t\t(");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ",\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine("\t\t\t);");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateSelectByServicesReturningInfo
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Create a stored procedure for each foreign key
                foreach (ArrayList compositeKeyList in table.ForeignKeys.Values)
                {
                    // Create the method name
                    StringBuilder stringBuilder = new StringBuilder(255);

                    stringBuilder.Append("SelectInfoBy");
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];

                        if (i > 0)
                        {
                            stringBuilder.Append("_" + Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                        else
                        {
                            stringBuilder.Append(Utility.FormatPascal(column.ProgrammaticAlias));
                        }
                    }
                    String methodName = stringBuilder.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\t//[DataObjectMethodAttribute(DataObjectMethodType.Select, true)]");

                    streamWriter.WriteLine("\t\tpublic BindingListView<" + className + "Contract> " + methodName + "(" + className + "Contract info) \r\n\t\t{");
                    
                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn " + className + "Controller." + methodName + "(info.To" + className + "Info()).ToBindingListViewOfContract();");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }
        #endregion Service Generated Class

        #region Service Custom Class
        /// <summary>
        /// Creates a WCF Service Custom class.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="path">Path where the class should be created.</param>
        /// <param name="settings">User settings.</param>
        public override Boolean CreateWcfServiceCustomClass
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, className + ".Custom." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetWcfServiceCustomClass(table, settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the WCF Service Custom class.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="settings">User settings.</param>
        /// <returns>A formatted app / web config snippet.</returns>
        protected override String GetWcfServiceCustomClass
        (
            Table table,
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "WcfServiceCustom.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                    returnValue = returnValue.Replace("#Namespace#", settings.Namespace + ".");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                    returnValue = returnValue.Replace("#Namespace#", "");
                }
                returnValue = returnValue.Replace("#Class#", className);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Service Custom Class

        #region Service Extensions Class
        /// <summary>
        /// Creates a WCF service extension class.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="settings"></param>
        public override Boolean CreateWcfServiceExtensionClass
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "ServiceExtensions." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System.Runtime.Serialization;");
                streamWriter.WriteLine("using System.ServiceModel;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " \r\n{");
                }

                #region Service Class Extensions
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\tinternal static class " + className + "ServiceExtensions\r\n\t{");

                // Append the access methods
                if (!CreateToListOfContractServiceExtension(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create ToListOfContract Service Extension.");
                }
                if (!CreateToContractServiceExtension(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create ToContract Service Extension.");
                }
                if (!CreateToInfoServiceExtension(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create ToInfo Service Extension.");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");
                #endregion Service Class Extensions

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }

        protected override Boolean CreateToListOfContractServiceExtension
        (
            Table table, 
            StreamWriter streamWriter, 
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Converts from List of " + className + "Info to List of " + className + "Contract.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\tinternal static BindingListView<" + className + "Contract> ToBindingListViewOfContract(this BindingListView<" + className + "Info> infoList) \r\n\t\t{ ");

                    streamWriter.WriteLine("\t\t\tBindingListView<" + className + "Contract> returnValue = new BindingListView<" + className + "Contract>();");
                    streamWriter.WriteLine("\t\t\tforeach (" + className + "Info info in infoList) \r\n\t\t\t{");

                    streamWriter.WriteLine("\t\t\t\treturnValue.Add");

                    streamWriter.WriteLine("\t\t\t\t\t(");
                    streamWriter.WriteLine("\t\t\t\t\tinfo.To" + className + "Contract()");

                    streamWriter.WriteLine("\t\t\t\t\t);");

                    streamWriter.WriteLine("\t\t\t}");
                    streamWriter.WriteLine("\t\t\treturn returnValue;");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateToContractServiceExtension
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Converts from " + className + "Info to " + className + "Contract.");
                    streamWriter.WriteLine("\t\t/// </summary>");

                    streamWriter.WriteLine("\t\tinternal static " + className + "Contract To" + className + "Contract(this " + className + "Info info) \r\n\t\t{");

                    streamWriter.WriteLine("\t\t\treturn new " + className + "Contract");
                    streamWriter.WriteLine("\t\t\t(");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "info.") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");


                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }

        protected override Boolean CreateToInfoServiceExtension
        (
            Table table,
            StreamWriter streamWriter,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                    className = Utility.FilterPathCharacters(className);

                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Converts from " + className + "Contract to " + className + "Info.");
                    streamWriter.WriteLine("\t\t/// </summary>");

                    streamWriter.WriteLine("\t\tinternal static " + className + "Info To" + className + "Info(this " + className + "Contract info) \r\n\t\t{");

                    streamWriter.WriteLine("\t\t\treturn new " + className + "Info");
                    streamWriter.WriteLine("\t\t\t(");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "info.") + ",\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 3));
                    streamWriter.WriteLine("\t\t\t);");


                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }
        #endregion Service Extensions Class

        #region Service Client Extensions Class
        /// <summary>
        /// Creates a WCF service client extension class.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="settings"></param>
        public override Boolean CreateWcfServiceClientExtensionClass
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            StreamWriter streamWriter = default(StreamWriter);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "ServiceClientExtensions." + CODE_FILE_EXT));
                streamWriter.WriteLine("using System.Collections.Generic;");
                streamWriter.WriteLine("using " + settings.Namespace + "." + className + "Service;");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("namespace " + settings.Namespace + " \r\n{");
                }

                #region Service Class Extensions
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\tinternal static class " + className + "ServiceClientExtensions\r\n\t{");

                // Append the access methods
                if (!CreateToListOfContractServiceClientExtension(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create ToListOfContract Service Client Extension.");
                }

                // Close out the class 
                streamWriter.WriteLine("\t}");
                #endregion Service Class Extensions

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("}");
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
            }
            finally
            {
                if (streamWriter != null)
                {
                    // Flush and close the stream
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            return returnValue;
        }

        protected override Boolean CreateToListOfContractServiceClientExtension
        (
            Table table, 
            StreamWriter streamWriter, 
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                if (table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t/// <summary>");
                    streamWriter.WriteLine("\t\t/// Converts from Array of " + className + "Contract to List of " + className + "Contract.");
                    streamWriter.WriteLine("\t\t/// </summary>");
                    streamWriter.WriteLine("\t\tinternal static BindingListView<" + className + "Contract> ToBindingListViewOfContract(this " + className + "Contract[] infoList) \r\n\t\t{ ");

                    streamWriter.WriteLine("\t\t\tBindingListView<" + className + "Contract> returnValue = new BindingListView<" + className + "Contract>();");
                    streamWriter.WriteLine("\t\t\tforeach (" + className + "Contract info in infoList) \r\n\t\t\t{");

                    streamWriter.WriteLine("\t\t\t\treturnValue.Add");

                    streamWriter.WriteLine("\t\t\t\t\t(");
                    streamWriter.WriteLine("\t\t\t\t\tinfo");

                    streamWriter.WriteLine("\t\t\t\t\t);");

                    streamWriter.WriteLine("\t\t\t}");
                    streamWriter.WriteLine("\t\t\treturn returnValue;");

                    // Append the method footer
                    streamWriter.WriteLine("\t\t}");
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
            }
            return returnValue;
        }
        #endregion Service Client Extensions Class

        #region Service Client App-Code Class
        /// <summary>
        /// Creates a WCF service application-code class.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="path"></param>
        /// <param name="settings"></param>
        public override Boolean CreateWcfServiceClientAppCodeClass
        (
            Table table,
            String path,
            Settings settings
        )
        {
            Boolean returnValue = default(Boolean);
            try
            {
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                using (StreamWriter streamWriter = new StreamWriter(Path.Combine(path, className + "ServiceClientApplication." + CODE_FILE_EXT)))
                {
                    streamWriter.Write(GetWcfServiceClientAppCodeClass(table, settings));
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
            }
            return returnValue;
        }

        /// <summary>
        /// Returns the contents of the WCF service application-code class.
        /// </summary>
        /// <param name="table">Instance of the Table class that represents the table this class will be created for.</param>
        /// <param name="settings">User settings.</param>
        /// <returns>A formatted app / web config snippet.</returns>
        protected override String GetWcfServiceClientAppCodeClass
        (
            Table table,
            Settings settings
        )
        {
            String returnValue = default(String);
            try
            {
                Boolean IsNameSpace = (settings.Namespace.Trim() != String.Empty);

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                String classVariableName = Utility.CleanWhitespace(Utility.FormatCamel(table.ProgrammaticAlias + "" + settings.ClassSuffix));
                classVariableName = Utility.FilterPathCharacters(classVariableName);

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "WcfServiceClientApplication.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "namespace " + settings.Namespace + " {");
                    returnValue = returnValue.Replace("#EndNamespace#", "}");
                    returnValue = returnValue.Replace("#Namespace#", settings.Namespace + ".");
                }
                else
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "");
                    returnValue = returnValue.Replace("#EndNamespace#", "");
                    returnValue = returnValue.Replace("#Namespace#", "");
                }
                returnValue = returnValue.Replace("#Class#", className);
                returnValue = returnValue.Replace("#ClassVariable#", classVariableName);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                        99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Service Client App-Code Class

        #region Utility
        /// <summary>
        /// Creates a String for a method parameter representing the specified column.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <returns>String containing parameter information of the specified column for a method call.</returns>
        public override String CreateMethodFormalParameter
        (
            Column column
        )
        {
            String returnValue = default(String);
            String dotNetTypeName = default(String);
            try
            {
                dotNetTypeName = GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false);
                returnValue = String.Format("{0} {1}", dotNetTypeName, column.ProgrammaticAlias);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                Log.Write(
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name,
                    Log.FormatEntry(String.Format("Column: {0}", column.ProgrammaticAlias), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String for a SqlParameter representing the specified column.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        protected override String CreateStoredProcedureActualParameterString
        (
            Column column,
            String columnPrefix
        )
        {
            String returnValue = default(String);
            try
            {
                returnValue = String.Format("new {0}(\"{1}\", NullHandler.HandleAppNull({2}{3}, DBNull.Value))", GeneratorModel.dbUtility.DotNetFrameworkClientLibParameter(), GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias), columnPrefix, column.ProgrammaticAlias);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                Log.Write(
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name,
                    Log.FormatEntry(String.Format("Column: {0}\r\nPrefix: {1}", column.ProgrammaticAlias, columnPrefix), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
            return returnValue;
        }

        /// <summary>
        /// Creates a String for a info parameter representing the specified column.
        /// </summary>
        /// <param name="column">Object that stores the information for the column the parameter represents.</param>
        /// <returns>String containing SqlParameter information of the specified column for a method call.</returns>
        protected override String CreateInfoActualParameterString
        (
            Column column,
            String columnPrefix
        )
        {
            String returnValue = default(String);
            try
            {
                returnValue = String.Format("{0}{1}", columnPrefix, column.ProgrammaticAlias);
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                Log.Write(
                    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Module.Name,
                    Log.FormatEntry(String.Format("Column: {0}\r\nPrefix: {1}", column.ProgrammaticAlias, columnPrefix), System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name),
                    System.Diagnostics.EventLogEntryType.Error,
                    99);
                throw ex;
            }
            return returnValue;
        }
        #endregion Utility
    }
}
