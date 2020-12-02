#define Nullable
using System;
using System.Data;
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using Ssepan.Utility;

namespace DataTierGeneratorPlusLibrary
{
	internal sealed class SqlUtility  : DbUtility
	{
		private const String SP_PARAMETER_PREFIX = "@"; //in Oracle, this would be a : (colon)
		private const String CODE_EXTENSION = "sql";//in Oracle, this might be "ora"; in Access it might be "qry"
//		//this is the prefix used in the name of the Built-In db helper library
		private const String DBUTIL_FILE_DB_PRE = "Sql"; //in Oracle, this would be Ora; in Access or MySql it would be OleDb
		private const String DOTNETFRAMEWORK_CLIENTLIB = "SqlClient"; //in Oracle, this would be OracleClient; in Access or MySql it would be OleDb
		private const String DOTNETFRAMEWORK_CLIENTLIB_CONNECTION = "SqlConnection"; //in Oracle, this would be OracleConnection; in Access or MySql it would be OleDbConnection
		private const String DOTNETFRAMEWORK_CLIENTLIB_TRANSACTION = "SqlTransaction"; //in Oracle, this would be OracleTransaction; in Access or MySql it would be OleDbTransaction
		private const String DOTNETFRAMEWORK_CLIENTLIB_COMMAND = "SqlCommand"; //in Oracle, this would be OracleCommand; in Access or MySql it would be OleDbCommand
		private const String DOTNETFRAMEWORK_CLIENTLIB_PARAMETER = "SqlParameter"; //in Oracle, this would be OracleParameter; in Access or MySql it would be OleDbParameter
		private const String DOTNETFRAMEWORK_CLIENTLIB_DATAREADER = "SqlDataReader"; //in Oracle, this would be OracleDataReader; in Access or MySql it would be OleDbDataReader
		private const String DOTNETFRAMEWORK_CLIENTLIB_DATAADAPTER = "SqlDataAdapter"; //in Oracle, this would be OracleDataAdapter; in Access or MySql it would be OleDbDataAdapter
#if Nullable
        private const String CODE_NULLABLE_SHORTCUT = "?";
#else
        private const String CODE_NULLABLE_SHORTCUT = "";
#endif

		public SqlUtility() 
		{
		}

//		public override String DbUtilFileDbPrefix()
//		{
//			return DBUTIL_FILE_DB_PRE;
//		}

		public override String DotNetFrameworkClientLib()
		{
			return DOTNETFRAMEWORK_CLIENTLIB;
		}

		public override String DotNetFrameworkClientLibConnection()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_CONNECTION;
		}

		public override String DotNetFrameworkClientLibTransaction()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_TRANSACTION;
		}

		public override String DotNetFrameworkClientLibCommand()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_COMMAND;
		}

		public override String DotNetFrameworkClientLibParameter()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_PARAMETER;
		}

		public override String DotNetFrameworkClientLibDataReader()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_DATAREADER;
		}

		public override String DotNetFrameworkClientLibDataAdapter()
		{
			return DOTNETFRAMEWORK_CLIENTLIB_DATAADAPTER;
		}

		/// <summary>
		/// Returns the query that should be used for retrieving the list of tables for the specified database.
		/// </summary>
		/// <param name="databaseName">The database to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
		public override String GetTableQuery
        (
            String databaseName
        ) 
		{
            String returnValue = default(String);
            try
            {
                returnValue = Utility.GetResource("DataTierGeneratorPlusLibrary.Templates." + DBUTIL_FILE_DB_PRE + "TableMetadata." + CODE_EXTENSION, "#DatabaseName#", databaseName);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }
		
		/// <summary>
		/// Returns the query that should be used for retrieving the list of columns for the specified table.
		/// </summary>
		/// <param name="databaseName">The table to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
		public override String GetColumnQuery
        (
            Table table
        ) 
		{
            String returnValue = default(String);
            try
            {
                returnValue = Utility.GetResource("DataTierGeneratorPlusLibrary.Templates." + DBUTIL_FILE_DB_PRE + "ColumnMetadata." + CODE_EXTENSION, "#TableName#", table.Name);
                returnValue = returnValue.Replace("#TableSchema#", table.Schema);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
		/// </summary>
		/// <param name="name">Name of the resource to retrieve.</param>
		/// <param name="databaseName">The name of the database to be used.</param>
		/// <param name="grantLoginName">The name of the user to be used.</param>
		/// <returns>The queries that should be used to create the specified database login.</returns>
        public override String GetUserQueries
        (
            String databaseName, 
            String grantLoginName
        ) 
        {
            String returnValue = default(String);
            try
            {
                returnValue = Utility.GetResource("DataTierGeneratorPlusLibrary.Templates." + DBUTIL_FILE_DB_PRE + "UserCreate." + CODE_EXTENSION);
                returnValue = returnValue.Replace("#DatabaseName#", databaseName);
                returnValue = returnValue.Replace("#UserName#", grantLoginName);
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Retrieves a connection String corresponding to the configured DBMS and Authentication model.
		/// </summary>
		/// <param name="model">User model.</param>
		/// <returns>The connection string, with parameters replaced.</returns>
		public override String GetConnectionString
        (
            GeneratorModel model
        )
		{
            String returnValue = default(String);
            try
            {
                if (model.SQLAuthentication)
                {
                    returnValue = "Provider=sqloledb;Data Source=" + model.Server + ";Initial Catalog=" + model.Database + ";User Id=" + model.SQLLogin + ";Password=" + model.Password + ";";
                }
                else
                {
                    returnValue = "Provider=sqloledb;Data Source=" + model.Server + ";Initial Catalog=" + model.Database + ";Integrated Security=SSPI;";
                }
                //one of these would be implemented in a DBMS-specific copy of this class.
                //MSsql:
                //"Provider=sqloledb;Data Source=#Server#;Initial Catalog=#Database#;User Id=#Login#;Password=#Password#;"
                //"Provider=sqloledb;Data Source=#Server#;Initial Catalog=#Database#;Integrated Security=SSPI;"
                //
                //oracle:
                //"Provider=msdaora;Data Source=#Database#;User Id=#Login#;Password=#Password#;"
                //"Provider=OraOLEDB.Oracle;Data Source=#Database#;OSAuthent=1;"
                //
                //mysql:
                //"Provider=MySQLProv;Data Source=#Database#;User Id=#Login#;Password=#Password#;" 
                //n/a
                //
                //msaccess:
                //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=#Database#;Jet OLEDB:Database Password=#Password#;"
                //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=#Database#;User Id=admin;Password=;"
                //
                //sybase:
                //"Provider=Sybase.ASEOLEDBProvider;Srvr=#Server#,5000;Catalog=#Database#;User Id=#Login#;Password=#Password#" 
                //na
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }
				
		/// <summary>
		/// Retrieves the foreign key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="table">Name of the table that foreign keys should be checked for.</param>
		/// <returns>DataReader containing the foreign key information for the specified table.</returns>
		public override DataTable GetForeignKeyList
        (
            OleDbConnection connection, 
            Table table
        ) 
		{
            DataTable returnValue = default(DataTable);
            try
            {
                OleDbParameter parameter;

                using (OleDbCommand command = new OleDbCommand("sp_fkeys", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("pktable_name"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_name", DataRowVersion.Current, DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("pktable_owner"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_owner", DataRowVersion.Current, DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("pktable_qualifier"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "pktable_qualifier", DataRowVersion.Current, DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("fktable_name"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_name", DataRowVersion.Current, table.Name);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("fktable_owner"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_owner", DataRowVersion.Current, DBNull.Value);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("fktable_qualifier"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "fktable_qualifier", DataRowVersion.Current, DBNull.Value);
                    command.Parameters.Add(parameter);

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                    returnValue = new DataTable();
                    dataAdapter.Fill(returnValue);
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
		/// Retrieves the primary key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="table">Name of the table that primary keys should be checked for.</param>
		/// <returns>DataReader containing the primary key information for the specified table.</returns>
		public override DataTable GetPrimaryKeyList
        (
            OleDbConnection connection, 
            Table table
        ) 
		{
            DataTable returnValue = default(DataTable);
            try
            {
                OleDbParameter parameter;

                using (OleDbCommand command = new OleDbCommand("sp_pkeys", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("table_name"), OleDbType.VarWChar, 128, ParameterDirection.Input, false, 0, 0, "table_name", DataRowVersion.Current, table.Name);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("table_owner"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "table_owner", DataRowVersion.Current, table.Schema);
                    command.Parameters.Add(parameter);
                    parameter = new OleDbParameter(GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName("table_qualifier"), OleDbType.VarWChar, 128, ParameterDirection.Input, true, 0, 0, "table_qualifier", DataRowVersion.Current, DBNull.Value/*table.Catalog*/);
                    command.Parameters.Add(parameter);

                    OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
                    returnValue = new DataTable();
                    dataAdapter.Fill(returnValue);
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
		/// Creates a String containing the parameter declaration for a stored procedure based on the parameters passed in.
		/// </summary>
		/// <param name="column">Object that stores the information for the column the parameter represents.</param>
		/// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
		public override String CreateStoredProcedureFormalParameterString
        (
            Column column
        ) 
        {
			String columnName;
            String returnValue = default(String);

            try
            {
                columnName = column.ProgrammaticAlias;

                switch (column.Type.ToLower())
                {
                    case "binary":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "bigint":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "bit":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "char":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "cursor":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "datetime":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "datetime2":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "decimal":
                        if (column.Scale.Length == 0)
                            returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Precision + ")";
                        else
                            returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                        break;
                    case "float":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Precision + ")";
                        break;
                    case "geography":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "geometry":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "hierarchyid":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "image":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "int":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "money":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "nchar":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "ntext":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "nvarchar":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "numeric":
                        if (column.Scale.Length == 0)
                            returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Precision + ")";
                        else
                            returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Precision + ", " + column.Scale + ")";
                        break;
                    case "real":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "smalldatetime":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "smallint":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "smallmoney":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "sql_variant":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "sysname":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "table":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "text":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "timestamp":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "tinyint":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "varbinary":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "uniqueidentifier":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    case "varchar":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type + "(" + column.Length + ")";
                        break;
                    case "xml":
                        returnValue = "" + GeneratorModel.DatabaseUtility.FormatStoredProcedureParameterName(columnName) + " " + column.Type;
                        break;
                    default:	// Unknown data type
                        throw (new Exception("Invalid SQL Server data type specified: " + column.Type));
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
		/// Matches a .Net data type to a System.Data.DbType.
		/// </summary>
        /// <param name="column">column info.</param>
        /// <param name="nonNullable">force method to return non-nullable variant</param>
        /// <returns>String containing System.Data.DbType.</returns>
		public override String GetDotNetTypeNameFromDbTypeName
        (
            Column column, 
            Boolean nonNullable
            )
		{
            String returnValue = default(String);
            String sqlType = column.Type;
            try
            {
                switch (sqlType.ToLower())
                {
                    case "binary":
                        returnValue = "Object";
                        break;
                    case "bigint":
                        returnValue = "Int64";
                        break;
                    case "bit":
                        returnValue = "Boolean";
                        break;
                    case "char":
                        returnValue = "String";//unsupported
                        break;
                    case "cursor":
                        returnValue = "String";//unsupported
                        break;
                    case "datetime":
                        returnValue = "DateTime";
                        break;
                    case "datetime2":
                        returnValue = "DateTime";
                        break;
                    case "decimal":
                        returnValue = "Decimal";
                        break;
                    case "float":
                        returnValue = "Double";
                        break;
                    case "geography":
                        returnValue = "Object";//unsupported
                        break;
                    case "geometry":
                        returnValue = "Object";//unsupported
                        break;
                    case "hierarchyid":
                        returnValue = "Object";//unsupported
                        break;
                    case "image":
                        returnValue = "Byte[]";//unsupported
                        break;
                    case "int":
                        returnValue = "Int32";
                        break;
                    case "money":
                        returnValue = "Decimal";
                        break;
                    case "nchar":
                        returnValue = "String";
                        break;
                    case "ntext":
                        returnValue = "String";//unsupported
                        break;
                    case "nvarchar":
                        returnValue = "String";
                        break;
                    case "numeric":
                        returnValue = "Decimal";
                        break;
                    case "real":
                        returnValue = "Single";
                        break;
                    case "smalldatetime":
                        returnValue = "DateTime";
                        break;
                    case "smallint":
                        returnValue = "Int16";
                        break;
                    case "smallmoney":
                        returnValue = "Decimal";
                        break;
                    case "sql_variant":
                        returnValue = "Object";
                        break;
                    case "sysname":
                        returnValue = "String";
                        break;
                    case "table":
                        returnValue = "Object";//unsupported
                        break;
                    case "text":
                        returnValue = "String";//unsupported
                        break;
                    case "timestamp":
                        returnValue = "DateTime";//unsupported
                        break;
                    case "tinyint":
                        returnValue = "Byte";
                        break;
                    case "uniqueidentifier":
                        returnValue = "Guid";
                        break;
                    case "varbinary":
                        returnValue = "Object";
                        break;
                    case "varchar":
                        returnValue = "String";//unsupported
                        break;
                    case "xml":
                        returnValue = "String";//unsupported
                        break;
                    default:	// Unknown data type
                        throw (new Exception("Invalid SQL Server data type specified: " + sqlType));
                }
                Type dotNetType = Type.GetType("System." + returnValue);//error getting type System.Binary for sql type image
                if ((!nonNullable) && (column.IsNullable) && (dotNetType.IsValueType))
                {
                    returnValue += CODE_NULLABLE_SHORTCUT;
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
		/// Applies any DBMS-specific formating to name of stored procedure parameter
		/// </summary>
		/// <param name="parameterName">String representing the name of the parameter.</param>
		/// <returns>String containing formatted parameter name.</returns>
		public override String FormatStoredProcedureParameterName
        (
            String parameterName
        )
		{
            String returnValue = default(String);
            try
            {
                returnValue = SP_PARAMETER_PREFIX + parameterName;
            }
            catch (Exception ex)
            {
                Log.Write(ex, MethodBase.GetCurrentMethod(), EventLogEntryType.Error);
                throw;
            }
            return returnValue;
        }
			
	}
}
