using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Diagnostics;
using Ssepan.Utility;
namespace DataTierGeneratorPlus 
{
	class SqlGenerator: DbGenerator 
	{
		private const String CODE_FOLDER = "SQL";
		private const String CODE_EXTENSION = "sql";//in Oracle, this might be "ora"

        private String _codeFolder = CODE_FOLDER;
        public override String CodeFolder
        {
            get { return _codeFolder; }
        }

		public SqlGenerator() {}
		
		/// <summary>
		/// Creates the SQL script that is responsible for granting the specified login access to the specified database.
		/// </summary>
		/// <param name="databaseName">The name of the database that the login will be created for.</param>
		/// <param name="path">Path where the script should be created.</param>
		/// <param name="settings">User settings.</param>
        public override Boolean CreateUserQueries
		(
			String databaseName, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (settings.GrantUser.Length > 0)
                {
                    String fileName;

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, "GrantUserPermissions." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.Write(GeneratorModel.dbUtility.GetUserQueries(databaseName, settings.GrantUser));
                    }
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    EventLogEntryType.Error,
                        99);
            }
            return returnValue;
        }

		/// <summary>
		/// Creates all stored procedure SQL scripts for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		public override Boolean CreateStoredProcedures
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (!CreateInsertStoredProcedure(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create Insert Stored Procedure.");
                }
                if (!CreateUpdateStoredProcedure(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create Update Stored Procedure.");
                }
                if (!CreateDeleteStoredProcedure(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create Delete Stored Procedure.");
                }
                if (!CreateDeleteByStoredProcedures(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create DeleteBy Stored Procedures.");
                }
                if (!CreateSelectStoredProcedure(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create Select Stored Procedure.");
                }
                if (!CreateSelectAllStoredProcedure(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create SelectAll Stored Procedure.");
                }
                if (!CreateSelectByStoredProcedures(table, path, settings))
                {
                    throw new ApplicationException("Unable to Create SelectBy Stored Procedures.");
                }

                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    EventLogEntryType.Error,
                        99);
            }
            return returnValue;
        }
		
		/// <summary>
		/// Creates an insert stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateInsertStoredProcedure
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                // Create the stored procedure name
                String procedureName = settings.SPPrefix + table.Name + "Insert";
                String fileName;

                procedureName = Utility.FilterPathCharacters(procedureName);

                // Determine the file name to be used
                if (settings.CreateMultipleFiles)
                {
                    fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                }
                else
                {
                    fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                }

                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    // Create the seperator
                    if (!settings.CreateMultipleFiles)
                    {
                        writer.WriteLine();
                        writer.WriteLine("/******************************************************************************");
                        writer.WriteLine("******************************************************************************/");
                    }

                    // Create the drop statment
                    writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                    writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                    writer.WriteLine("GO");
                    writer.WriteLine();

                    // Create the SQL for the stored procedure
                    writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                    //DEBUG:fix bug wherein a key in last column causes problems with handling of comma
                    // Create the parameter list
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];
                        if (!column.IsRowGuidCol && !column.IsIdentity)
                        {
                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                        }
                    }
                    writer.WriteLine(")");

                    writer.WriteLine();
                    writer.WriteLine("AS");
                    writer.WriteLine();
                    writer.WriteLine("SET NOCOUNT ON");
                    writer.WriteLine();

                    // Initialize all RowGuidCol columns
                    foreach (Column column in table.Columns)
                    {
                        if (column.IsRowGuidCol)
                        {
                            writer.WriteLine("DECLARE " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + " uniqueidentifier");
                            writer.WriteLine("SET " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + " = NEWID()");
                            writer.WriteLine();
                            break;
                        }
                    }

                    writer.WriteLine("INSERT INTO [" + table.Schema + "].[" + table.Name + "] (");

                    // Create the parameter list
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        // Ignore any identity columns
                        if (!column.IsIdentity)
                        {
                            // Append the column name as a parameter of the insert statement
                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t[" + column.Name + "],");
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "]");
                            }
                        }
                    }

                    writer.WriteLine(") VALUES (");

                    // Create the values list
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        Column column = (Column)table.Columns[i];

                        // Is the current column an identity column?
                        if (!column.IsIdentity)
                        {
                            // Append the necessary line breaks and commas
                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }
                    }

                    writer.WriteLine(")");

                    // Should we include a line for returning the identity?
                    foreach (Column column in table.Columns)
                    {
                        // Is the current column an identity column?
                        if (column.IsIdentity)
                        {
                            writer.WriteLine();
                            writer.WriteLine("SELECT SCOPE_IDENTITY() AS " + column.Name);
                            break;
                        }
                        else if (column.IsRowGuidCol)
                        {
                            writer.WriteLine();
                            writer.WriteLine("SELECT " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.Name) + " AS " + column.Name);
                            break;
                        }
                    }

                    writer.WriteLine("GO");

                    // Create the grant statement, if a user was specified
                    if (settings.GrantUser.Length > 0)
                    {
                        writer.WriteLine();
                        writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                        writer.WriteLine("GO");
                    }
                }


                returnValue = true;
            }
            catch (Exception ex)
            {
                Log.Write(
                    ex,
                    System.Reflection.MethodBase.GetCurrentMethod(),
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates an update stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateUpdateStoredProcedure
		(
			Table table, 
			String path, 
			Settings settings
	    ) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.Columns.Count != table.PrimaryKeys.Count && table.Columns.Count != table.ForeignKeys.Count)
                {
                    // Create the stored procedure name
                    String procedureName = settings.SPPrefix + table.Name + "Update";
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                        // Create the parameter list
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Column column = (Column)table.Columns[i];

                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                        }
                        writer.WriteLine(")");

                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("UPDATE");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");
                        writer.WriteLine("SET");

                        // Create the set statement
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Column column = (Column)table.Columns[i];

                            // Ignore Identity and RowGuidCol columns
                            if (!table.PrimaryKeys.Contains(column))
                            {
                                if (i < (table.Columns.Count - 1))
                                {
                                    writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ",");
                                }
                                else
                                {
                                    writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                                }
                            }
                        }

                        writer.WriteLine("WHERE");

                        // Create the where clause
                        for (int i = 0; i < table.PrimaryKeys.Count; i++)
                        {
                            Column column = (Column)table.PrimaryKeys[i];

                            if (i > 0)
                            {
                                writer.Write("\tAND [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                            else
                            {
                                writer.Write("\t [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }

                        //BEGIN alternate code suggestion - how to handle nulls. (David Rogers, SourceForge)
                        //					// Create the where clause 
                        //					for (int i = 0; i < compositeKeyList.Count; i++) 
                        //					{ 
                        //						Column column = (Column) compositeKeyList[i]; 
                        // 
                        //						if (i > 0)  
                        //						{ 
                        //							writer.WriteLine("\tAND "); 
                        //						}  
                        //						else  
                        //						{ 
                        //							writer.WriteLine("\t"); 
                        //						} 
                        //						writer.WriteLine("(COALESCE([" + column.Name + "], " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ") IS NULL " +  
                        //							"OR [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ")"); 
                        //					}
                        //END alternate code suggestion - how to handle nulls.

                        writer.WriteLine();

                        writer.WriteLine("GO");

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates an delete stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateDeleteStoredProcedure
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0)
                {
                    // Create the stored procedure name
                    String procedureName = settings.SPPrefix + table.Name + "Delete";
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                        // Create the parameter list
                        for (int i = 0; i < table.PrimaryKeys.Count; i++)
                        {
                            Column column = (Column)table.PrimaryKeys[i];

                            if (i < (table.PrimaryKeys.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                        }
                        writer.WriteLine(")");

                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("DELETE FROM");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");
                        writer.WriteLine("WHERE");

                        // Create the where clause
                        for (int i = 0; i < table.PrimaryKeys.Count; i++)
                        {
                            Column column = (Column)table.PrimaryKeys[i];

                            if (i > 0)
                            {
                                writer.WriteLine("\tAND [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }

                        //BEGIN alternate code suggestion - how to handle nulls. (David Rogers, SourceForge)
                        //					// Create the where clause 
                        //					for (int i = 0; i < compositeKeyList.Count; i++) 
                        //					{ 
                        //						Column column = (Column) compositeKeyList[i]; 
                        // 
                        //						if (i > 0)  
                        //						{ 
                        //							writer.WriteLine("\tAND "); 
                        //						}  
                        //						else  
                        //						{ 
                        //							writer.WriteLine("\t"); 
                        //						} 
                        //						writer.WriteLine("(COALESCE([" + column.Name + "], " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ") IS NULL " +  
                        //							"OR [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ")"); 
                        //					}
                        //END alternate code suggestion - how to handle nulls.

                        writer.WriteLine("GO");

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates one or more delete stored procedures SQL script for the specified table and its foreign keys
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateDeleteByStoredProcedures
		(
			Table table, 
			String path, 
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
                    stringBuilder.Append(settings.SPPrefix + table.Name + "DeleteBy");

                    // Create the parameter list
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

                    String procedureName = stringBuilder.ToString();
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                        // Create the parameter list
                        for (int i = 0; i < compositeKeyList.Count; i++)
                        {
                            Column column = (Column)compositeKeyList[i];

                            if (i < (compositeKeyList.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                        }
                        writer.WriteLine(")");

                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("DELETE FROM");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");
                        writer.WriteLine("WHERE");

                        // Create the where clause
                        for (int i = 0; i < compositeKeyList.Count; i++)
                        {
                            Column column = (Column)compositeKeyList[i];

                            if (i > 0)
                            {
                                writer.WriteLine("\tAND [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }

                        writer.WriteLine("GO");

                        //BEGIN alternate code suggestion - how to handle nulls. (David Rogers, SourceForge)
                        //					// Create the where clause 
                        //					for (int i = 0; i < compositeKeyList.Count; i++) 
                        //					{ 
                        //						Column column = (Column) compositeKeyList[i]; 
                        // 
                        //						if (i > 0)  
                        //						{ 
                        //							writer.WriteLine("\tAND "); 
                        //						}  
                        //						else  
                        //						{ 
                        //							writer.WriteLine("\t"); 
                        //						} 
                        //						writer.WriteLine("(COALESCE([" + column.Name + "], " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ") IS NULL " +  
                        //							"OR [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ")"); 
                        //					}
                        //END alternate code suggestion - how to handle nulls.

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates an select stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateSelectStoredProcedure
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count)
                {
                    // Create the stored procedure name
                    String procedureName = settings.SPPrefix + table.Name + "Select";
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                        // Create the parameter list
                        for (int i = 0; i < table.PrimaryKeys.Count; i++)
                        {
                            Column column = (Column)table.PrimaryKeys[i];

                            if (i == (table.PrimaryKeys.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                        }

                        writer.WriteLine(")");

                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("SELECT");

                        // Create the list of columns
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Column column = (Column)table.Columns[i];

                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t[" + column.Name + "],");
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "]");
                            }
                        }

                        writer.WriteLine("FROM");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");
                        writer.WriteLine("WHERE");

                        // Create the where clause
                        for (int i = 0; i < table.PrimaryKeys.Count; i++)
                        {
                            Column column = (Column)table.PrimaryKeys[i];

                            if (i > 0)
                            {
                                writer.WriteLine("\tAND [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }

                        //BEGIN alternate code suggestion - how to handle nulls. (David Rogers, SourceForge)
                        //					// Create the where clause 
                        //					for (int i = 0; i < compositeKeyList.Count; i++) 
                        //					{ 
                        //						Column column = (Column) compositeKeyList[i]; 
                        // 
                        //						if (i > 0)  
                        //						{ 
                        //							writer.WriteLine("\tAND "); 
                        //						}  
                        //						else  
                        //						{ 
                        //							writer.WriteLine("\t"); 
                        //						} 
                        //						writer.WriteLine("(COALESCE([" + column.Name + "], " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ") IS NULL " +  
                        //							"OR [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ")"); 
                        //					}
                        //END alternate code suggestion - how to handle nulls.

                        writer.WriteLine("GO");

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates an select all stored procedure SQL script for the specified table
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateSelectAllStoredProcedure
		(
			Table table, 
			String path, 
			Settings settings
		) 
		{
            Boolean returnValue = default(Boolean);
            try
            {
                if (table.PrimaryKeys.Count > 0 && table.ForeignKeys.Count != table.Columns.Count)
                {
                    // Create the stored procedure name
                    String procedureName = settings.SPPrefix + table.Name + "SelectAll";
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("SELECT");

                        // Create the list of columns
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Column column = (Column)table.Columns[i];

                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t[" + column.Name + "],");
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "]");
                            }
                        }

                        writer.WriteLine("FROM");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");

                        writer.WriteLine("GO");

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

		/// <summary>
		/// Creates one or more select stored procedures SQL script for the specified table and its foreign keys
		/// </summary>
		/// <param name="table">Instance of the Table class that represents the table this stored procedure will be created for.</param>
		/// <param name="path">Path where the stored procedure script should be created.</param>
		/// <param name="settings">User settings.</param>
		protected override Boolean CreateSelectByStoredProcedures
		(
			Table table, 
			String path, 
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
                    stringBuilder.Append(settings.SPPrefix + table.Name + "SelectBy");

                    // Create the parameter list
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

                    String procedureName = stringBuilder.ToString();
                    String fileName;

                    procedureName = Utility.FilterPathCharacters(procedureName);

                    // Determine the file name to be used
                    if (settings.CreateMultipleFiles)
                    {
                        fileName = Path.Combine(path, procedureName + "." + CODE_EXTENSION);
                    }
                    else
                    {
                        fileName = Path.Combine(path, "StoredProcedures." + CODE_EXTENSION);
                    }

                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        // Create the seperator
                        if (!settings.CreateMultipleFiles)
                        {
                            writer.WriteLine();
                            writer.WriteLine("/******************************************************************************");
                            writer.WriteLine("******************************************************************************/");
                        }

                        // Create the drop statment
                        writer.WriteLine("if exists (select * from dbo.sysobjects where id = object_id(N'[" + table.Schema + "].[" + procedureName + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                        writer.WriteLine("\tdrop procedure [" + table.Schema + "].[" + procedureName + "]");
                        writer.WriteLine("GO");
                        writer.WriteLine();

                        // Create the SQL for the stored procedure
                        writer.WriteLine("CREATE PROCEDURE [" + table.Schema + "].[" + procedureName + "] (");

                        // Create the parameter list
                        for (int i = 0; i < compositeKeyList.Count; i++)
                        {
                            Column column = (Column)compositeKeyList[i];

                            if (i < (compositeKeyList.Count - 1))
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column) + ",");
                            }
                            else
                            {
                                writer.WriteLine("\t" + GeneratorModel.dbUtility.CreateStoredProcedureFormalParameterString(column));
                            }
                        }
                        writer.WriteLine(")");

                        writer.WriteLine();
                        writer.WriteLine("AS");
                        writer.WriteLine();
                        writer.WriteLine("SET NOCOUNT ON");
                        writer.WriteLine();
                        writer.WriteLine("SELECT");

                        // Create the list of columns
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            Column column = (Column)table.Columns[i];

                            if (i < (table.Columns.Count - 1))
                            {
                                writer.WriteLine("\t[" + column.Name + "],");
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "]");
                            }
                        }

                        writer.WriteLine("FROM");
                        writer.WriteLine("\t[" + table.Schema + "].[" + table.Name + "]");
                        writer.WriteLine("WHERE");

                        // Create the where clause
                        for (int i = 0; i < compositeKeyList.Count; i++)
                        {
                            Column column = (Column)compositeKeyList[i];

                            if (i > 0)
                            {
                                writer.WriteLine("\tAND [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                            else
                            {
                                writer.WriteLine("\t[" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias));
                            }
                        }

                        //BEGIN alternate code suggestion - how to handle nulls. (David Rogers, SourceForge)
                        //					// Create the where clause 
                        //					for (int i = 0; i < compositeKeyList.Count; i++) 
                        //					{ 
                        //						Column column = (Column) compositeKeyList[i]; 
                        // 
                        //						if (i > 0)  
                        //						{ 
                        //							writer.WriteLine("\tAND "); 
                        //						}  
                        //						else  
                        //						{ 
                        //							writer.WriteLine("\t"); 
                        //						} 
                        //						writer.WriteLine("(COALESCE([" + column.Name + "], " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ") IS NULL " +  
                        //							"OR [" + column.Name + "] = " + GeneratorModel.dbUtility.FormatStoredProcedureParameterName(column.ProgrammaticAlias) + ")"); 
                        //					}
                        //END alternate code suggestion - how to handle nulls.

                        writer.WriteLine("GO");

                        // Create the grant statement, if a user was specified
                        if (settings.GrantUser.Length > 0)
                        {
                            writer.WriteLine();
                            writer.WriteLine("GRANT EXECUTE ON [" + table.Schema + "].[" + procedureName + "] TO [" + settings.GrantUser + "]");
                            writer.WriteLine("GO");
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
                    EventLogEntryType.Error,
                        99);
                //throw;
            }
            return returnValue;
        }

	}
}
