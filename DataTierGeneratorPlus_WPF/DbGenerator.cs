using System;

namespace DataTierGeneratorPlus 
{
	abstract class DbGenerator : IDbGenerator
	{
        abstract public String CodeFolder
        {
            get;
        }
		
		/// <summary>
		/// Creates the SQL script that is responsible for granting the specified login access to the specified database.
		/// </summary>
		/// <param name="databaseName">The name of the database that the login will be created for.</param>
		/// <param name="path">Path where the script should be created.</param>
		/// <param name="settings">User settings.</param>
        public abstract Boolean CreateUserQueries
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
        public abstract Boolean CreateStoredProcedures
		(
			Table table, 
			String path, 
			Settings settings
		); 

		//the rest are private, and so do not belong in interface; maybe in a base class
		
		/// <summary>
		/// Creates an insert stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateInsertStoredProcedure
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates an update stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateUpdateStoredProcedure
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates an delete stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateDeleteStoredProcedure
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates one or more delete stored procedures SQL script for the specified table and its foreign keys
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateDeleteByStoredProcedures
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates an select stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateSelectStoredProcedure
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates an select all stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateSelectAllStoredProcedure
			(
			Table table, 
			String path, 
			Settings settings
			) ;

		/// <summary>
		/// Creates one or more select stored procedures SQL script for the specified table and its foreign keys
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
        protected abstract Boolean CreateSelectByStoredProcedures
			(
			Table table, 
			String path, 
			Settings settings
			) ;
	}
}
