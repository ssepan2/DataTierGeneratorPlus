using System;
using System.Collections;
using System.IO;
using System.Text;
using Ssepan.Utility;

namespace DataTierGeneratorPlus 
{
	class VbGeneratorBuiltIn : VsGenerator
	{
		private const String CODE_FOLDER = "VB";
        private const String CODE_FILE_EXT = "vb";
        private const String DOTNET_LANGUAGE_ABBREVIATION = "VB";
        private const String TEMPLATE_FILE_VS_PRE = "Vb";

        private String _codeFolder = CODE_FOLDER;
        public override String CodeFolder
        {
            get { return _codeFolder; }
        }

		public VbGeneratorBuiltIn() {}

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
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "NullHandler.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "SortComparer.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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
                Boolean IsNameSpace = (settings.Namespace.Trim() != "");

                returnValue = Utility.GetResource("DataTierGeneratorPlus.Templates." + TEMPLATE_FILE_VS_PRE + "BindingListView.txt");

                if (IsNameSpace)
                {
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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
        /// Creates a VB table structure class for all of the table's fields .
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
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Collections");
                streamWriter.WriteLine("Imports System.ComponentModel ");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }

                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t''' <summary>");
                streamWriter.WriteLine("\t''' Class that stores table fields.");
                streamWriter.WriteLine("\t''' </summary>");
                streamWriter.WriteLine("\tPublic Class " + className + " ");

                //do default constructor
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Default constructor.  ");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\tPublic Sub New() ");
                streamWriter.WriteLine("\t\tEnd Sub ");

                //begin constructor with values
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Constructor with values.  ");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\tPublic Sub New( _");

                // Append the method formal parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t" + CreateMethodFormalParameter(column) + ", _\r\n");
                    }
                }
                builder.Remove(builder.Length - 5, 1);
                streamWriter.Write(builder.ToString(0, builder.Length));//removes comma, but leaves space, underscore, and newline

                streamWriter.WriteLine("\t\t)");

                // Append the private field assignment statements
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        streamWriter.WriteLine("\t\t\t_" + column.ProgrammaticAlias + " = " + column.ProgrammaticAlias + "");
                    }
                }

                //end constructor with values
                streamWriter.WriteLine("\t\tEnd Sub");

                // Create a private variable for each of the columns
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' Private variables for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\tPrivate " + " _" + column.ProgrammaticAlias + " As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + "");
                }

                // Create a property accessor for each of the columns
                Boolean IsPrimaryKey = false;
                Boolean IsIdentity = false;
                Boolean IsNullable = false;

                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' Public properties for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];

                    IsPrimaryKey = column.IsPrimaryKey;
                    IsIdentity = (column.IsIdentity || column.IsRowGuidCol);
                    IsNullable = column.IsNullable;

                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' " + column.ProgrammaticAlias + "  ");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectFieldAttribute(" + Utility.FormatPascal(IsPrimaryKey.ToString()) + ", " + Utility.FormatPascal(IsIdentity.ToString()) + ", " + Utility.FormatPascal(IsNullable.ToString()) + ")> _");
                    streamWriter.WriteLine("\t\tPublic Property " + column.ProgrammaticAlias + " As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + "");
                    streamWriter.WriteLine("\t\t\tGet ");
                    streamWriter.WriteLine("\t\t\t\tReturn _" + column.ProgrammaticAlias + " ");
                    streamWriter.WriteLine("\t\t\tEnd Get");
                    streamWriter.WriteLine("\t\t\tSet(ByVal Value As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + ") ");
                    streamWriter.WriteLine("\t\t\t\t_" + column.ProgrammaticAlias + " = Value");
                    streamWriter.WriteLine("\t\t\tEnd Set");
                    streamWriter.WriteLine("\t\tEnd Property");
                }

                // Close out the class 
                streamWriter.WriteLine("\tEnd Class");

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
		/// Creates a VB data access  class for all of the table's stored procedures generated automatically.
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

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Controller" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + ".Generated." + CODE_FILE_EXT));
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Collections.Generic");
                streamWriter.WriteLine("Imports System.Data");
                streamWriter.WriteLine("Imports System.Data." + GeneratorModel.dbUtility.DotNetFrameworkClientLib() + "");
                streamWriter.WriteLine("Imports System.ComponentModel");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }

                streamWriter.WriteLine("\t<DataObject(true)> _");
                streamWriter.WriteLine("\tPartial Public Class " + className + " ");
                streamWriter.WriteLine("\t\tProtected Sub New() ");
                streamWriter.WriteLine("\t\tEnd Sub");

                // Create an enum for accessing each of the columns using an integer value, which increases performance
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t'Public enums for column positions on the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\tPublic Enum ColumnIndex As Integer");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\t" + Utility.FormatPascal(column.ProgrammaticAlias) + " = " + i.ToString() + "");
                }
                streamWriter.WriteLine("\t\tEnd Enum");

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
                streamWriter.WriteLine("\tEnd Class");

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tPublic Shared Function Insert(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tPublic Shared Function Insert(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tPublic Shared Sub Insert(");
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
                streamWriter.Write("");

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine(")");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(") As " + returnType);
                        break;
                    case "Guid":
                        streamWriter.WriteLine(") As " + returnType);
                        break;
                }

                // start procedure body  ...

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t'//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn CType(DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");//, Integer)
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t'//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn CType(DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");//, Guid)
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");
                }

                // Append the parameters
                builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");//minus newline, underscore, space, and comma, then re-append the space and newline


                // Close parameter list and append conversion type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine("\t\t\t)");
                        break;
                    case "Integer":
                        streamWriter.WriteLine("\t\t\t), " + returnType + ")");
                        break;
                    case "Guid":
                        streamWriter.WriteLine("\t\t\t), " + returnType + ")");
                        break;
                }

                // Append the method footer
                if (returnVoid)
                {
                    streamWriter.WriteLine("\t\tEnd Sub");
                }
                else
                {
                    streamWriter.WriteLine("\t\tEnd Function");
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
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tPublic Shared Function InsertInfo(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tPublic Shared Function InsertInfo(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tPublic Shared Sub InsertInfo(");
                }

                // Append the info parameter
                streamWriter.Write("ByVal info As " + className + ")");

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine("");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(" As " + returnType);
                        break;
                    case "Guid":
                        streamWriter.WriteLine(" As " + returnType);
                        break;
                }

                // start procedure body  ...

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t'//Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn CType(DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");//, Integer)
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t'//Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn CType(DatabaseUtility.ExecuteScalar(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");//, Guid)
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Insert]\", _");
                }

                // Append the parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ", _\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");//minus newline, underscore, space, and comma, then re-append the space and newline


                // Close parameter list and append conversion type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine("\t\t\t)");
                        break;
                    case "Integer":
                        streamWriter.WriteLine("\t\t\t), " + returnType + ")");
                        break;
                    case "Guid":
                        streamWriter.WriteLine("\t\t\t), " + returnType + ")");
                        break;
                }

                // Append the method footer
                if (returnVoid)
                {
                    streamWriter.WriteLine("\t\tEnd Sub");
                }
                else
                {
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _");

                    streamWriter.Write("\t\tPublic Shared Sub Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(")");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Update]\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");


                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _");

                    streamWriter.WriteLine("\t\tPublic Shared Sub UpdateInfo(ByVal info As " + className + ")");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Update]\", _");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");


                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");

                    streamWriter.Write("\t\tPublic Shared Sub Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(")");

                    // Append the stored procedure execution
                    streamWriter.Write("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Delete]\", _\r\n");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _");

                    streamWriter.WriteLine("\t\tPublic Shared Sub DeleteInfo(ByVal info As " + className + ")");

                    // Append the stored procedure execution
                    streamWriter.Write("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Delete]\", _\r\n");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");

                    streamWriter.Write("\t\tPublic Shared Sub " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(")");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");

                    streamWriter.WriteLine("\t\tPublic Shared Sub " + methodName + "(ByVal info As " + className + ")");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tDatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\", _");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "info.") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");

                    streamWriter.Write("\t\tPublic Shared Function SelectDR(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Select]\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");

                    streamWriter.Write("\t\tPublic Shared Function SelectDS(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") As DataSet ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "Select]\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");

                    streamWriter.WriteLine("\t\tPublic Shared Function SelectInfo(ByVal info As " + className + ") As BindingListView(Of " + className + ") ");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tUsing dr As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " = SelectDR (");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("info." + column.ProgrammaticAlias + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2));
                    streamWriter.WriteLine(") ");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\tReturn LoadListDR(dr)");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\tEnd Using");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");
                    streamWriter.WriteLine("\t\tPublic Shared Function SelectDRAll() As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + "  ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "SelectAll]\")");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");
                    streamWriter.WriteLine("\t\tPublic Shared Function SelectDSAll() As DataSet  ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"[" + table.Schema + "].[" + table.Name + "SelectAll]\")");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");
                    streamWriter.WriteLine("\t\tPublic Shared Function SelectInfoAll() As BindingListView(Of " + className + ")  ");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tUsing dr As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " = SelectDRAll(");

                    // Append the parameters
                    streamWriter.WriteLine(") ");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\tReturn LoadListDR(dr)");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\tEnd Using");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");

                    streamWriter.Write("\t\tPublic Shared Function " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");

                    streamWriter.Write("\t\tPublic Shared Function " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") As DataSet ");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\treturn DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, \"" + procedureName + "\", _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateStoredProcedureActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    String methodName2 = stringBuilder2.ToString();

                    // Create the select function based on keys
                    // Append the method header
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");

                    streamWriter.WriteLine("\t\tPublic Shared Function " + methodName + "(ByVal info As " + className + ") As BindingListView(Of " + className + ") ");

                    // Append the using header and stored procedure execution
                    streamWriter.Write("\t\t\tUsing dr As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + " = " + methodName2 + "(");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("info." + Utility.FormatCamel(column.ProgrammaticAlias) + ", \r\n");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 4));
                    streamWriter.WriteLine(") ");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\tReturn LoadListDR(dr)");

                    // Append the using footer
                    streamWriter.WriteLine("\t\t\tEnd Using");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                     streamWriter.WriteLine("\t\t''' <summary>");
                     streamWriter.WriteLine("\t\t''' Loads all records from the [" + table.Schema + "].[" + table.Name + "] table into List of " + className + ".");
                     streamWriter.WriteLine("\t\t''' </summary>");
                     streamWriter.WriteLine("\t\tProtected Shared Function LoadListDR(ByVal dr As " + GeneratorModel.dbUtility.DotNetFrameworkClientLibDataReader() + ") As BindingListView(Of " + className + ")  ");

                     streamWriter.WriteLine("\t\t\tDim infoList As BindingListView(Of " + className + ") = New BindingListView(Of " + className + ")");
                     streamWriter.WriteLine("\t\t\tWhile (dr.Read())");

                     streamWriter.WriteLine("\t\t\t\tinfoList.Add _");

                     streamWriter.WriteLine("\t\t\t\t\t( _");
                     streamWriter.WriteLine("\t\t\t\t\tNew " + className + " _");

                     streamWriter.WriteLine("\t\t\t\t\t\t( _");

                     // Append the method formal parameters
                     StringBuilder builder = new StringBuilder();
                     foreach (Column column in table.Columns)
                     {
                         if (column.IsIdentity == false && column.IsRowGuidCol == false)
                         {
                             builder.Append("\t\t\t\t\t\tNullHandler.HandleDbNull(dr(\"" + Utility.FormatCamel(column.ProgrammaticAlias) + "\")), _\r\n");
                         }
                     }
                     builder.Remove(builder.Length - 5, 1);
                     streamWriter.Write(builder.ToString(0, builder.Length));//removes comma, but leaves space, underscore, and newline

                     streamWriter.WriteLine("\t\t\t\t\t\t) _");

                     streamWriter.WriteLine("\t\t\t\t\t)");

                     streamWriter.WriteLine("\t\t\tEnd While");
                     streamWriter.WriteLine("\t\t\tReturn infoList");

                     // Append the method footer
                     streamWriter.WriteLine("\t\tEnd Function");
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
        /// Creates an empty VB data access  class for all of the table's stored procedures created by hand.
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
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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
        /// Creates a VB contract class for all of the table's fields .
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
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Data");
                streamWriter.WriteLine("Imports System.Runtime.Serialization");
                streamWriter.WriteLine("Imports System.ServiceModel");
                streamWriter.WriteLine("'Imports System.ComponentModel ");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }
                
                #region Interface
                //start interface
                streamWriter.WriteLine("\t''' <summary>");
                streamWriter.WriteLine("\t''' Class that defines the table operations.");
                streamWriter.WriteLine("\t''' </summary>");
                streamWriter.WriteLine("\t<ServiceContract()> _");
                streamWriter.WriteLine("\tPublic Interface I" + className + "Service ");

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
                streamWriter.WriteLine("\tEnd Interface");
                #endregion Interface

                #region Contract
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t''' <summary>");
                streamWriter.WriteLine("\t''' Class that stores table fields.");
                streamWriter.WriteLine("\t''' </summary>");
                streamWriter.WriteLine("\t<DataContract()> _");
                streamWriter.WriteLine("\tPublic Class " + className + "Contract ");

                //do default constructor
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Default constructor.  ");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\tPublic Sub New() ");
                streamWriter.WriteLine("\t\tEnd Sub ");

                //begin constructor with values
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Constructor with values.  ");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\tPublic Sub New( _");

                // Append the method formal parameters
                StringBuilder builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t" + CreateMethodFormalParameter(column) + ", _\r\n");
                    }
                }
                builder.Remove(builder.Length - 5, 1);
                streamWriter.Write(builder.ToString(0, builder.Length));//removes comma, but leaves space, underscore, and newline

                streamWriter.WriteLine("\t\t)");

                // Append the private field assignment statements
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        streamWriter.WriteLine("\t\t\t_" + column.ProgrammaticAlias + " = " + column.ProgrammaticAlias + "");
                    }
                }

                //end constructor with values
                streamWriter.WriteLine("\t\tEnd Sub");

                // Create a private variable for each of the columns
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' Private variables for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];
                    streamWriter.WriteLine("\t\tPrivate " + " _" + column.ProgrammaticAlias + " As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + "");
                }

                // Create a property accessor for each of the columns
                Boolean IsPrimaryKey = false;
                Boolean IsIdentity = false;
                Boolean IsNullable = false;

                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t' Public properties for columns in the " + table.Name + " table.");
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    Column column = (Column)table.Columns[i];

                    IsPrimaryKey = column.IsPrimaryKey;
                    IsIdentity = (column.IsIdentity || column.IsRowGuidCol);
                    IsNullable = column.IsNullable;

                    streamWriter.WriteLine();
                    streamWriter.WriteLine("\t\t'<DataObjectFieldAttribute(" + Utility.FormatPascal(IsPrimaryKey.ToString()) + ", " + Utility.FormatPascal(IsIdentity.ToString()) + ", " + Utility.FormatPascal(IsNullable.ToString()) + ")> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' " + column.ProgrammaticAlias + "  ");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<DataMember()> _");
                    streamWriter.WriteLine("\t\tPublic Property " + column.ProgrammaticAlias + " As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + "");
                    streamWriter.WriteLine("\t\t\tGet ");
                    streamWriter.WriteLine("\t\t\t\tReturn _" + column.ProgrammaticAlias + " ");
                    streamWriter.WriteLine("\t\t\tEnd Get");
                    streamWriter.WriteLine("\t\t\tSet(ByVal Value As " + GeneratorModel.dbUtility.GetDotNetTypeNameFromDbTypeName(column, false) + ") ");
                    streamWriter.WriteLine("\t\t\t\t_" + column.ProgrammaticAlias + " = Value");
                    streamWriter.WriteLine("\t\t\tEnd Set");
                    streamWriter.WriteLine("\t\tEnd Property");
                }

                // Close out the class 
                streamWriter.WriteLine("\tEnd Class");
                #endregion Contract

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\t<OperationContract()> _");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tFunction Insert(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tFunction Insert(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tSub Insert(");
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
                streamWriter.Write("");

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine(")");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(") As " + returnType);
                        break;
                    case "Guid":
                        streamWriter.WriteLine(") As " + returnType);
                        break;
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
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");
                streamWriter.WriteLine("\t\t<OperationContract()> _");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tFunction InsertInfo(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tFunction InsertInfo(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tSub InsertInfo(");
                }

                // Append the info parameter
                streamWriter.Write("ByVal info As " + className + "Contract)");

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine("");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(" As " + returnType);
                        break;
                    case "Guid":
                        streamWriter.WriteLine(" As " + returnType);
                        break;
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.Write("\t\tSub Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(")");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.WriteLine("\t\tSub UpdateInfo(ByVal info As " + className + "Contract)");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.Write("\t\tSub Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(")");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.WriteLine("\t\tSub DeleteInfo(ByVal info As " + className + "Contract)");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.Write("\t\tSub " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.WriteLine("\t\tSub " + methodName + "(ByVal info As " + className + "Contract)");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.Write("\t\tFunction SelectDS(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") As DataSet ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.WriteLine("\t\tFunction SelectInfo(ByVal info As " + className + "Contract) As BindingListView(Of " + className + "Contract) ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");
                    streamWriter.WriteLine("\t\tFunction SelectDSAll() As DataSet  ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");
                    streamWriter.WriteLine("\t\tFunction SelectInfoAll() As BindingListView(Of " + className + "Contract)  ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.Write("\t\tFunction " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") As DataSet ");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<OperationContract()> _");

                    streamWriter.WriteLine("\t\tFunction " + methodName + "(ByVal info As " + className + "Contract) As BindingListView(Of " + className + "Contract) ");
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
        /// Creates a VB language WCF Service class for all of the table's stored procedures generated automatically.
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

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "Service" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + ".Generated." + SVC_FILE_EXT + "." + CODE_FILE_EXT));
                streamWriter.WriteLine("Imports System");
                streamWriter.WriteLine("Imports System.Data");
                streamWriter.WriteLine("Imports System.Runtime.CompilerServices");
                streamWriter.WriteLine("Imports System.Runtime.Serialization");
                streamWriter.WriteLine("Imports System.ServiceModel");
                streamWriter.WriteLine("'Imports System.ComponentModel");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }

                #region Service Class
                //start class
                streamWriter.WriteLine("\t'<DataObject(true)> _");
                streamWriter.WriteLine("\tPartial Public Class " + className + " ");
                streamWriter.WriteLine("\t\tImplements I" + className + " ");
                streamWriter.WriteLine("\t\tProtected Sub New() ");
                streamWriter.WriteLine("\t\tEnd Sub");

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
                streamWriter.WriteLine("\tEnd Class");
                #endregion Service Class

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + ""));
                className = Utility.FilterPathCharacters(className);

                // Append the method header
                streamWriter.WriteLine();
                streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _");
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tPublic Function Insert(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tPublic Function Insert(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tPublic Sub Insert(");
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

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine(") _");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(") As " + returnType + " _");
                        break;
                    case "Guid":
                        streamWriter.WriteLine(") As " + returnType + " _");
                        break;
                }
                streamWriter.WriteLine("\t\t Implements I" + className + "Service.Insert");

                // start procedure body  ...

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t'Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.Insert( _");//, Integer)
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t'Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.Insert( _");//, Guid)
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.Insert( _");
                }

                // Append the parameters
                builder = new StringBuilder();
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity == false && column.IsRowGuidCol == false)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\r\n");
                    }
                }
                streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");//minus newline, underscore, space, and comma, then re-append the space and newline
                streamWriter.WriteLine("\t\t\t)");

                // Append the method footer
                if (returnVoid)
                {
                    streamWriter.WriteLine("\t\tEnd Sub");
                }
                else
                {
                    streamWriter.WriteLine("\t\tEnd Function");
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
                streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _");
                streamWriter.WriteLine("\t\t''' <summary>");
                streamWriter.WriteLine("\t\t''' Inserts a record into the [" + table.Schema + "].[" + table.Name + "] table.");
                streamWriter.WriteLine("\t\t''' </summary>");

                // Determine the return type of the insert function
                Boolean returnVoid = true;
                String returnType = "void";
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        streamWriter.Write("\t\tPublic Function InsertInfo(");
                        returnType = "Integer";
                        returnVoid = false;
                        break;
                    }
                    else if (column.IsRowGuidCol)
                    {
                        streamWriter.Write("\t\tPublic Function InsertInfo(");
                        returnType = "Guid";
                        returnVoid = false;
                        break;
                    }
                }

                // If the return type hasn't been set, return void
                if (returnVoid)
                {
                    streamWriter.Write("\t\tPublic Sub InsertInfo(");
                }

                // Append the info parameter
                streamWriter.Write("ByVal info As " + className + "Contract");

                // Close parameter list and append the type, if any
                switch (returnType)
                {
                    case "void":
                        streamWriter.WriteLine(") _");
                        break;
                    case "Integer":
                        streamWriter.WriteLine(") As " + returnType + " _");
                        break;
                    case "Guid":
                        streamWriter.WriteLine(") As " + returnType + " _");
                        break;
                }
                streamWriter.WriteLine("\t\t Implements I" + className + "Service.InsertInfo");

                // start procedure body  ...

                Boolean executeScalar = false;
                // Append the parameter value extraction
                foreach (Column column in table.Columns)
                {
                    if (column.IsIdentity || column.IsRowGuidCol)
                    {
                        if (column.IsIdentity)
                        {
                            streamWriter.WriteLine("\t\t\t'Execute the query and return the new Guid");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.InsertInfo( _");//, Integer)
                            executeScalar = true;
                            break;
                        }
                        else
                        {
                            streamWriter.WriteLine("\t\t\t'Execute the query and return the new identity value");
                            streamWriter.WriteLine("\t\t\treturn " + className + "Controller.InsertInfo( _");//, Guid)
                            executeScalar = true;
                            break;
                        }
                        //executeScalar = true;
                    }
                }

                if (executeScalar == false)
                {
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.InsertInfo( _");
                }

                // Append the parameters
                streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info() _");

                streamWriter.WriteLine("\t\t\t)");

                // Append the method footer
                if (returnVoid)
                {
                    streamWriter.WriteLine("\t\tEnd Sub");
                }
                else
                {
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.Write("\t\tPublic Sub Update(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.Update");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.Update( _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");


                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Updates a record in the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\tPublic Sub UpdateInfo(ByVal info As " + className + "Contract) _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.UpdateInfo");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller.UpdateInfo( _");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info() _");

                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.Write("\t\tPublic Sub Delete(");
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.Delete");

                    // Append the stored procedure execution
                    streamWriter.Write("\t\t\t" + className + "Controller.Delete( _\r\n");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a composite primary key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\tPublic Sub DeleteInfo(ByVal info As " + className + "Contract) _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.DeleteInfo");

                    // Append the stored procedure execution
                    streamWriter.Write("\t\t\t" + className + "Controller.DeleteInfo( _\r\n");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info() _");

                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.Write("\t\tPublic Sub " + methodName + "(");

                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service." + methodName + "");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller." + methodName + "( _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Deletes a record from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\tPublic Sub " + methodName + "(ByVal info As " + className + "Contract) _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service." + methodName + "");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\t" + className + "Controller." + methodName + "( _");

                    // Append the parameters
                    streamWriter.WriteLine("\t\t\t\tinfo.To" + className + "Info() _");
                    
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Sub");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.Write("\t\tPublic Function SelectDS(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");

                    // Append the connection String parameter
                    streamWriter.Write("");

                    // Close parameter list
                    streamWriter.WriteLine(") As DataSet _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.SelectDS");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tReturn " + className + "Controller.SelectDS _\r\n\t\t\t( _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in table.PrimaryKeys)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects a single record from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\tPublic Function SelectInfo(ByVal info As " + className + "Contract) As BindingListView(Of " + className + "Contract) _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.SelectInfo");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\t\tReturn " + className + "Controller.SelectInfo(info.To" + className + "Info()).ToBindingListViewOfContract()");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\tPublic Function SelectDSAll() As DataSet  _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.SelectDSAll");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tReturn " + className + "Controller.SelectDSAll()");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\tPublic Function SelectInfoAll() As BindingListView(Of " + className + "Contract)  _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service.SelectInfoAll");

                    // Append the List-load execution
                    streamWriter.WriteLine("\t\t\tReturn " + className + "Controller.SelectInfoAll().ToBindingListViewOfContract()");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.Write("\t\tPublic Function " + methodName + "(");
                    // Append the method call parameters
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < compositeKeyList.Count; i++)
                    {
                        Column column = (Column)compositeKeyList[i];
                        builder.Append(CreateMethodFormalParameter(column) + ", ");
                    }
                    streamWriter.Write(builder.ToString(0, builder.Length - 2) + "");
                    streamWriter.WriteLine(") As DataSet _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service." + methodName + "");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tReturn " + className + "Controller." + methodName + "( _");

                    // Append the parameters
                    builder = new StringBuilder();
                    foreach (Column column in compositeKeyList)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "") + ", _\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 4) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _");
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Selects all records from the [" + table.Schema + "].[" + table.Name + "] table by a foreign key.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\tPublic Function " + methodName + "(ByVal info As " + className + "Contract) As BindingListView(Of " + className + "Contract) _");
                    streamWriter.WriteLine("\t\t Implements I" + className + "Service." + methodName + "");

                    // Append the stored procedure execution
                    streamWriter.WriteLine("\t\t\tReturn " + className + "Controller." + methodName + "(info.To" + className + "Info()).ToBindingListViewOfContract()");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    returnValue = returnValue.Replace("#BeginNamespace#", "NameSpace " + settings.Namespace + "");
                    returnValue = returnValue.Replace("#EndNamespace#", "End NameSpace");
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

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "ServiceExtensions." + CODE_FILE_EXT));
                streamWriter.WriteLine("Imports System.Runtime.CompilerServices");
                streamWriter.WriteLine("Imports System.Runtime.Serialization");
                streamWriter.WriteLine("Imports System.ServiceModel");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }

                #region Service Class Extensions
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\tFriend Module " + className + "ServiceExtensions ");

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
                streamWriter.WriteLine("\tEnd Module");
                #endregion Service Class Extensions

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Converts from List of " + className + "Info to List of " + className + "Contract.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<Extension()> _");
                    streamWriter.WriteLine("\t\tFriend Function ToBindingListViewOfContract(ByVal infoList As BindingListView(Of " + className + "Info)) As BindingListView(Of " + className + "Contract)  ");

                    streamWriter.WriteLine("\t\t\tDim returnValue As New BindingListView(Of " + className + "Contract) ");
                    streamWriter.WriteLine("\t\t\tFor Each info As " + className + "Info In infoList");

                    streamWriter.WriteLine("\t\t\t\treturnValue.Add _");

                    streamWriter.WriteLine("\t\t\t\t\t( _");
                    streamWriter.WriteLine("\t\t\t\t\tinfo.To" + className + "Contract() _");

                    streamWriter.WriteLine("\t\t\t\t\t)");

                    streamWriter.WriteLine("\t\t\tNext");
                    streamWriter.WriteLine("\t\t\tReturn returnValue");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t'''  Converts from " + className + "Info to " + className + "Contract.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\t<Extension()> _");
                    streamWriter.WriteLine("\t\tFriend Function To" + className + "Contract(ByVal info As " + className + "Info) As " + className + "Contract");

                    streamWriter.WriteLine("\t\t\tReturn New " + className + "Contract( _");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "info.") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t'''  Converts from " + className + "Contract to " + className + "Info.");
                    streamWriter.WriteLine("\t\t''' </summary>");

                    streamWriter.WriteLine("\t\t<Extension()> _");
                    streamWriter.WriteLine("\t\tFriend Function To" + className + "Info(ByVal info As " + className + "Contract) As " + className + "Info");

                    streamWriter.WriteLine("\t\t\tReturn New " + className + "Info( _");

                    // Append the parameters
                    StringBuilder builder = new StringBuilder();
                    foreach (Column column in table.Columns)
                    {
                        builder.Append("\t\t\t\t" + CreateInfoActualParameterString(column, "info.") + ", _\r\n");
                    }
                    streamWriter.WriteLine(builder.ToString(0, builder.Length - 5) + " _");
                    streamWriter.WriteLine("\t\t\t)");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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

                String className = Utility.CleanWhitespace(Utility.FormatPascal(table.ProgrammaticAlias + "" + settings.ClassSuffix));
                className = Utility.FilterPathCharacters(className);

                // Create the header for the class
                streamWriter = new StreamWriter(Path.Combine(path, className + "ServiceClientExtensions." + CODE_FILE_EXT));
                streamWriter.WriteLine("Imports System.Runtime.CompilerServices");
                streamWriter.WriteLine("Imports System.Collections.Generic");
                streamWriter.WriteLine("Imports " + settings.Namespace + "." + className + "Service");

                if (IsNameSpace)
                {
                    //do namespace
                    streamWriter.WriteLine();
                    streamWriter.WriteLine("'Namespace " + settings.Namespace + " ");
                }

                #region Service Class Extensions
                //start class
                streamWriter.WriteLine();
                streamWriter.WriteLine("\tFriend Module " + className + "ServiceClientExtensions ");

                // Append the access methods
                if (!CreateToListOfContractServiceClientExtension(table, streamWriter, settings))
                {
                    throw new ApplicationException("Unable to Create ToListOfContract Service Client Extension.");
                }
                
                // Close out the class 
                streamWriter.WriteLine("\tEnd Module");
                #endregion Service Class Extensions

                if (IsNameSpace)
                {
                    // Close out the namespace
                    streamWriter.WriteLine("'End Namespace");
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
                    streamWriter.WriteLine("\t\t''' <summary>");
                    streamWriter.WriteLine("\t\t''' Converts from Array of " + className + "Contract to List of " + className + "Contract.");
                    streamWriter.WriteLine("\t\t''' </summary>");
                    streamWriter.WriteLine("\t\t<Extension()> _");
                    streamWriter.WriteLine("\t\tFriend Function ToBindingListViewOfContract(ByVal infoList As " + className + "Contract()) As BindingListView(Of " + className + "Contract)  ");

                    streamWriter.WriteLine("\t\t\tDim returnValue As New BindingListView(Of " + className + "Contract) ");
                    streamWriter.WriteLine("\t\t\tFor Each info As " + className + "Contract In infoList");

                    streamWriter.WriteLine("\t\t\t\treturnValue.Add _");

                    streamWriter.WriteLine("\t\t\t\t\t( _");
                    streamWriter.WriteLine("\t\t\t\t\tinfo _");

                    streamWriter.WriteLine("\t\t\t\t\t)");

                    streamWriter.WriteLine("\t\t\tNext");
                    streamWriter.WriteLine("\t\t\tReturn returnValue");

                    // Append the method footer
                    streamWriter.WriteLine("\t\tEnd Function");
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
                    returnValue = returnValue.Replace("#BeginNamespace#", "Namespace " + settings.Namespace + " ");
                    returnValue = returnValue.Replace("#EndNamespace#", "End Namespace");
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
                returnValue = String.Format("{0} As {1}", column.ProgrammaticAlias, dotNetTypeName);
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
                returnValue = String.Format("New {0}(\"{1}\", NullHandler.HandleAppNull({2}{3}, DBNull.Value))", GeneratorModel.dbUtility.DotNetFrameworkClientLibParameter(), GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias), columnPrefix, column.ProgrammaticAlias);
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
