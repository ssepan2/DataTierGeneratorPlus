using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Cs_BuiltIn_WcfServiceApp {
	
	public sealed class DatabaseUtility {

        private static String _ConnectionStringKeyName   = "ConnectionString";
        private static SqlTransaction _TransactionObject   = null;
		
		private DatabaseUtility() {}

        /// <summary>
        /// Use ConnectionStringKeyName to let the utility look up the connection String from a config file.
        /// </summary>
        /// <value>
        /// Defaults to <c>ConnectionString</c>.
        /// </value>
        public static String ConnectionStringKeyName  
        {
            get { return _ConnectionStringKeyName; }
            set { _ConnectionStringKeyName = value; }
        }

        /// <summary>
        /// Set TransactionObject in code that calls this utilty to specify a Transaction object in advance, then retrieve and pass that object to this utilitie's Execute methods, along with a connection object.
        /// </summary>
        /// <remarks>
        /// This arrangement makes it possible for code (methods) that call this utility to require no connection or transaction parameters themselves, but to retrieve values that were set elsewhere.
        /// </remarks>
        /// <value>
        /// Defaults to <c>null</c>.
        /// </value>
        public static SqlTransaction TransactionObject 
        {  
            get { return _TransactionObject; }
            set  { _TransactionObject = value; }
        }
		
		public static String GetConnectionString() 
		{
			return GetConnectionString(ConnectionStringKeyName);
		}
		
		public static String GetConnectionString(String connectionStringKeyName) 
		{
			if (ConfigurationManager.ConnectionStrings[connectionStringKeyName] != null)
			{
               return ConfigurationManager.ConnectionStrings[connectionStringKeyName].ToString();
            }
            else
            {
                if (ConfigurationManager.AppSettings[connectionStringKeyName] != null)
                {
                    return ConfigurationManager.AppSettings[connectionStringKeyName].ToString();
                }
                else
                {
                    //if (Cs_BuiltIn_WcfServiceApp.Properties.Settings.Default[connectionStringKeyName] != null) //<--WinForms only
                    //{
                    //    return Cs_BuiltIn_WcfServiceApp.Properties.Settings.Default[connectionStringKeyName].ToString();
                    //}
                    //else
                    //{
                        return String.Empty;
                    //}
                }
            }
		}
		
		public static SqlConnection GetConnection() 
		{
			return new SqlConnection(GetConnectionString());
		}
		
		public static SqlConnection GetConnection(String connectionStringKeyName) 
		{
			return new SqlConnection(GetConnectionString(connectionStringKeyName));
		}
		
		public static SqlConnection GetConnection(Boolean open) 
		{
			SqlConnection connection = new SqlConnection(GetConnectionString());
			if (open) 
			{
				connection.Open();
			}
			return connection;
		}

		public static void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, String commandText) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				}
				
				try 
				{
					// Execute the query
					command.ExecuteNonQuery();
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}

		public static void ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, String commandText, params SqlParameter[] parameters) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				}
				
				try {
					// Execute the query
					command.ExecuteNonQuery();
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}
		
		public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, String commandText) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
					return command.ExecuteReader(CommandBehavior.CloseConnection);
				} 
				else 
				{
					return command.ExecuteReader();
				}				
			}
		}
		
		public static SqlDataReader ExecuteReader(SqlConnection connection, SqlTransaction transaction, String commandText, params SqlParameter[] parameters)
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
					return command.ExecuteReader(CommandBehavior.CloseConnection);
				} 
				else 
				{
					return command.ExecuteReader();
				}				
			}
		}
		
		public static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, String commandText) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				} 
			
				try 
				{
					// Execute the query
					return CreateDataSet(command);
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}
		
		public static DataSet ExecuteDataSet(SqlConnection connection, SqlTransaction transaction, String commandText, params SqlParameter[] parameters) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				} 
			
				try 
				{
					// Execute the query
					return CreateDataSet(command);
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}
		
		public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, String commandText) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				} 
			
				try 
				{
					// Execute the query
					return CreateDataTable(command);
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}
		
		public static DataTable ExecuteDataTable(SqlConnection connection, SqlTransaction transaction, String commandText, params SqlParameter[] parameters) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				} 
			
				try 
				{
					// Execute the query
					return CreateDataTable(command);
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}

		public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, String commandText) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				}
				
				try 
				{
					// Execute the query
					return command.ExecuteScalar();
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}

		public static object ExecuteScalar(SqlConnection connection, SqlTransaction transaction, String commandText, params SqlParameter[] parameters) 
		{
			ConnectionState previousState = connection.State;
			
			using (SqlCommand command = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)) 
			{
				// Open the database connection if it isn't already opened
				if (previousState == ConnectionState.Closed) 
				{
					connection.Open();
				}
				
				try 
				{
					// Execute the query
					return command.ExecuteScalar();
				} 
				finally 
				{
					// If the database connection was closed before the method call, close it again
					if (previousState == ConnectionState.Closed) 
					{
						connection.Close();
					}
				}
			}
		}

        //Portions below based on SharpCore.Data.SqlClientUtility.cs.
        //Microsoft Public License (Ms-PL) This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software. 
        //1. Definitions The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law. A "contribution" is the original software, or any additions or changes to the software. A "contributor" is any person that distributes its contribution under this license. "Licensed patents" are a contributor's patent claims that read directly on its contribution. 
        //2. Grant of Rights (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create. (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software. 
        //3. Conditions and Limitations (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks. (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically. (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software. (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license. (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
        #region "Ms-PL"
		#region CreateCommand
		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqlCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqlConnection"/> the <see cref="SqlCommand"/> should be executed on.</param>
		/// <param name="transaction">The <see cref="SqlTransaction"/> the <see cref="SqlCommand"/> should be executed with.</param>
		/// <param name="commandType">The command type.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <returns>An initialized <see cref="SqlCommand"/> instance.</returns>
		private static SqlCommand CreateCommand(SqlConnection connection, SqlTransaction transaction, CommandType commandType, String commandText)
		{
		    SqlCommand command = new SqlCommand();
		    command.Connection = connection;
		    command.CommandText = commandText;
		    command.CommandType = commandType;
		    command.Transaction = transaction;
		    return command;
		}

		/// <summary>
		/// Creates, initializes, and returns a <see cref="SqlCommand"/> instance.
		/// </summary>
		/// <param name="connection">The <see cref="SqlConnection"/> the <see cref="SqlCommand"/> should be executed on.</param>
		/// <param name="transaction">The <see cref="SqlTransaction"/> the <see cref="SqlCommand"/> should be executed with.</param>
		/// <param name="commandType">The command type.</param>
		/// <param name="commandText">The name of the stored procedure to execute.</param>
		/// <param name="parameters">The parameters of the stored procedure.</param>
		/// <returns>An initialized <see cref="SqlCommand"/> instance.</returns>
		private static SqlCommand CreateCommand(SqlConnection connection, SqlTransaction transaction, CommandType commandType, String commandText, params SqlParameter[] parameters)
		{
		    SqlCommand command = new SqlCommand();
		    command.Connection = connection;
		    command.CommandText = commandText;
		    command.CommandType = commandType;
		    command.Transaction = transaction;
			
		    if (parameters != null)
		    {
			    // Append each parameter to the command
			    foreach (SqlParameter parameter in parameters)
			    {
				    parameter.Value = NullHandler.HandleAppNull(parameter.Value, DBNull.Value); 
				    command.Parameters.Add(parameter);
			    }
		    }

		    return command;
		}
		
		#endregion CreateCommand

		#region CreateDataX
        private static DataSet CreateDataSet(SqlCommand command)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
        }

        private static DataTable CreateDataTable(SqlCommand command)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
        #endregion CreateDataX
        #endregion Ms-PL

	}

}
