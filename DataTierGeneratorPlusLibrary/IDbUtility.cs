using System;
using System.Data;
//using System.Data.SqlClient;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
namespace DataTierGeneratorPlusLibrary 
{
	public interface IDbUtility 
	{
//		String DbUtilFileDbPrefix();

		String DotNetFrameworkClientLib();
		String DotNetFrameworkClientLibConnection();
		String DotNetFrameworkClientLibTransaction();
		String DotNetFrameworkClientLibCommand();
		String DotNetFrameworkClientLibParameter();
		String DotNetFrameworkClientLibDataReader();
		String DotNetFrameworkClientLibDataAdapter();

		/// <summary>
		/// Returns the query that should be used for retrieving the list of tables for the specified database.
		/// </summary>
		/// <param name="databaseName">The database to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of tables for the specified database.</returns>
		String GetTableQuery(String databaseName) ;
		
		/// <summary>
		/// Returns the query that should be used for retrieving the list of columns for the specified table.
		/// </summary>
		/// <param name="databaseName">The table to be queried for.</param>
		/// <returns>The query that should be used for retrieving the list of columns for the specified table.</returns>
		String GetColumnQuery(Table table) ;

		/// <summary>
		/// Retrieves the specified manifest resource stream from the executing assembly as a string, replacing the specified old value with the specified new value.
		/// </summary>
		/// <param name="name">Name of the resource to retrieve.</param>
		/// <param name="databaseName">The name of the database to be used.</param>
		/// <param name="grantLoginName">The name of the user to be used.</param>
		/// <returns>The queries that should be used to create the specified database login.</returns>
		String GetUserQueries(String databaseName, String grantLoginName) ;

		/// <summary>
		/// Retrieves a connection String corresponding to the configured DBMS and Authentication model.
		/// </summary>
		/// <param name="model">User model.</param>
		/// <returns>The connection string, with parameters replaced.</returns>
		String GetConnectionString(GeneratorModel model);
				
		/// <summary>
		/// Retrieves the foreign key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="table">Name of the table that foreign keys should be checked for.</param>
		/// <returns>DataReader containing the foreign key information for the specified table.</returns>
		DataTable GetForeignKeyList(OleDbConnection connection, Table table) ;
		
		/// <summary>
		/// Retrieves the primary key information for the specified table.
		/// </summary>
		/// <param name="connection">The SqlConnection to be used when querying for the table information.</param>
		/// <param name="table">Name of the table that primary keys should be checked for.</param>
		/// <returns>DataReader containing the primary key information for the specified table.</returns>
		DataTable GetPrimaryKeyList(OleDbConnection connection, Table table); 

		/// <summary>
		/// Creates a String containing the parameter declaration for a stored procedure based on the parameters passed in.
		/// </summary>
		/// <param name="column">Object that stores the information for the column the parameter represents.</param>
		/// <returns>String containing parameter information of the specified column for a stored procedure.</returns>
		String CreateStoredProcedureFormalParameterString(Column column) ;

		/// <summary>
		/// Matches a .Net data type to a System.Data.DbType.
		/// </summary>
        /// <param name="column">column info.</param>
        /// <param name="nonNullable">force method to return non-nullable variant</param>
        /// <returns>String containing System.Data.DbType.</returns>
        String GetDotNetTypeNameFromDbTypeName(Column column, Boolean nonNullable);

        ///// <summary>
        ///// Matches a .Net data type to a System.Data.DbType.
        ///// </summary>
        ///// <param name="column">Object that stores the information for the column the parameter represents.</param>
        ///// <returns>String containing System.Data.DbType.</returns>
        //String GetDotNetTypeNameFromDbTypeName(Column column);

		/// <summary>
		/// Applies any DBMS-specific formating to name of stored procedure parameter
		/// </summary>
		/// <param name="parameterName">String representing the name of the parameter.</param>
		/// <returns>String containing formatted parameter name.</returns>
		String FormatStoredProcedureParameterName(String parameterName);
		
	}
}
