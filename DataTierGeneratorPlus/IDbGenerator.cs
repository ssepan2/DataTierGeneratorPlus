using System;

namespace DataTierGeneratorPlus 
{
	interface IDbGenerator 
	{
        String CodeFolder
        {
            get;
        }
		
		/// <summary>
		/// Creates the SQL script that is responsible for granting the specified login access to the specified database.
		/// </summary>
		/// <param name="databaseName">The name of the database that the login will be created for.</param>
		/// <param name="path">Path where the script should be created.</param>
		/// <param name="settings">User settings.</param>
        Boolean CreateUserQueries
			(
			String databaseName, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates all stored procedure SQL scripts for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        Boolean CreateStoredProcedures
		(
			Table table, 
			String path, 
			Settings settings
		); 

		//the rest are private, and so do not belong in interface; maybe in a base class
		
	}
}
