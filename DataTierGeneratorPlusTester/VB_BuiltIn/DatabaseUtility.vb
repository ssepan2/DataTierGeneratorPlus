Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

'NameSpace VB_BuiltIn 

    Public NotInheritable Class DatabaseUtility

        Private Shared _ConnectionStringKeyName As String = "ConnectionString"
        Private Shared _TransactionObject As SqlTransaction = Nothing

        Shared Sub New()
        End Sub

        ''' <summary>
        ''' Use ConnectionStringKeyName to let the utility look up the connection String from a config file.
        ''' </summary>
        ''' <value>
        ''' Defaults to <c>ConnectionString</c>.
        ''' </value>
        Public Shared Property ConnectionStringKeyName() As String
            Get
                Return _ConnectionStringKeyName
            End Get
            Set(ByVal Value As String)
                _ConnectionStringKeyName = Value
            End Set
        End Property

        ''' <summary>
        ''' Set TransactionObject in code that calls this utilty to specify a Transaction object in advance, then retrieve and pass that object to this utilitie's Execute methods, along with a connection object.
        ''' </summary>
        ''' <remarks>
        ''' This arrangement makes it possible for code (methods) that call this utility to require no connection or transaction parameters themselves, but to retrieve values that were set elsewhere.
        ''' </remarks>
        ''' <value>
        ''' Defaults to <c>Nothing</c>.
        ''' </value>
        Public Shared Property TransactionObject() As SqlTransaction
            Get
                Return _TransactionObject
            End Get
            Set(ByVal Value As SqlTransaction)
                _TransactionObject = Value
            End Set
        End Property

        Public Shared Function GetConnectionString() As String
            Return GetConnectionString(ConnectionStringKeyName)
        End Function

        Public Shared Function GetConnectionString(ByVal connectionStringKeyName As String) As String
            If Not (ConfigurationManager.ConnectionStrings(connectionStringKeyName) Is Nothing) Then

                Return ConfigurationManager.ConnectionStrings(connectionStringKeyName).ToString()

            Else

                If Not (ConfigurationManager.AppSettings(connectionStringKeyName) Is Nothing) Then

                    Return ConfigurationManager.AppSettings(connectionStringKeyName).ToString()

                Else

                    'If Not (VB_BuiltIn.Properties.Settings.Default[connectionStringKeyName] Is Nothing) Then '<--WinForms only
                    '    Return VB_BuiltIn.Properties.Settings.Default[connectionStringKeyName].ToString()
                    'Else
                        Return String.Empty
                    'End If

                End If
            End If
        End Function

        Public Shared Function GetConnection() As SqlConnection
            Return New SqlConnection(GetConnectionString())
        End Function

        'overload
        Public Shared Function GetConnection(ByVal connectionStringKeyName As String) As SqlConnection
            Return New SqlConnection(GetConnectionString(connectionStringKeyName))
        End Function

        Public Shared Function GetConnection(ByVal open As Boolean) As SqlConnection
            Dim connection As SqlConnection = New SqlConnection(GetConnectionString())
            If (open) Then
                connection.Open()
            End If
            Return connection
        End Function

        Public Shared Sub ExecuteNonQuery(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String)
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					command.ExecuteNonQuery()
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Sub

        Public Shared Sub ExecuteNonQuery(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String, ParamArray ByVal parameters As SqlParameter())
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					command.ExecuteNonQuery()
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Sub

        Public Shared Function ExecuteReader(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String) As SqlDataReader
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
					Return command.ExecuteReader(CommandBehavior.CloseConnection)
				Else
					Return command.ExecuteReader()
				End If
            End Using
        End Function

        Public Shared Function ExecuteReader(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String, ParamArray ByVal parameters As SqlParameter()) As SqlDataReader
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
					Return command.ExecuteReader(CommandBehavior.CloseConnection)
				Else
					Return command.ExecuteReader()
				End If
            End Using
        End Function

        Public Shared Function ExecuteDataSet(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String) As DataSet
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					Return CreateDataSet(command)
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        Public Shared Function ExecuteDataSet(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String, ParamArray ByVal parameters As SqlParameter()) As DataSet
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then

					connection.Open()
				End If

				Try
					' Execute the query
					Return CreateDataSet(command)
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        Public Shared Function ExecuteDataTable(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String) As DataTable
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					Return CreateDataTable(command)
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        Public Shared Function ExecuteDataTable(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String, ParamArray ByVal parameters As SqlParameter()) As DataTable
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then

					connection.Open()
				End If

				Try
					' Execute the query
					Return CreateDataTable(command)
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        Public Shared Function ExecuteScalar(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String) As Object
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					Return command.ExecuteScalar()
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        Public Shared Function ExecuteScalar(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction, ByVal commandText As String, ParamArray ByVal parameters As SqlParameter()) As Object
            Dim previousState As ConnectionState = connection.State

            Using command As SqlCommand = CreateCommand(connection, transaction, CommandType.StoredProcedure, commandText, parameters)
				' Open the database connection if it isn't already opened
				If (previousState = ConnectionState.Closed) Then
					connection.Open()
				End If

				Try
					' Execute the query
					Return command.ExecuteScalar()
				Finally
					' If the database connection was closed before the method call, close it again
					If (previousState = ConnectionState.Closed) Then
						connection.Close()
					End If
				End Try
            End Using
        End Function

        'Portions below based on SharpCore.Data.SqlClientUtility.cs.
        'Microsoft Public License (Ms-PL) This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software. 
        '1. Definitions The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law. A "contribution" is the original software, or any additions or changes to the software. A "contributor" is any person that distributes its contribution under this license. "Licensed patents" are a contributor's patent claims that read directly on its contribution. 
        '2. Grant of Rights (A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create. (B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software. 
        '3. Conditions and Limitations (A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks. (B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically. (C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software. (D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license. (E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
		#Region "Ms-PL"
		#Region "CreateCommand"

		''' <summary>
		''' Creates, initializes, and returns a <see cref="SqlCommand"/> instance.
		''' </summary>
		''' <param name="connection">The <see cref="SqlConnection"/> the <see cref="SqlCommand"/> should be executed on.</param>
		''' <param name="transaction">The <see cref="SqlTransaction"/> the <see cref="SqlCommand"/> should be executed with.</param>
		''' <param name="commandType">The command type.</param>
		''' <param name="commandText">The name of the stored procedure to execute.</param>
		''' <returns>An initialized <see cref="SqlCommand"/> instance.</returns>
		Private Shared Function CreateCommand(connection As SqlConnection, transaction As SqlTransaction, commandType As CommandType, commandText As String) As SqlCommand
			Dim command As New SqlCommand()
			command.Connection = connection
			command.CommandText = commandText
			command.CommandType = commandType
			command.Transaction = transaction
			Return command
		End Function

		''' <summary>
		''' Creates, initializes, and returns a <see cref="SqlCommand"/> instance.
		''' </summary>
		''' <param name="connection">The <see cref="SqlConnection"/> the <see cref="SqlCommand"/> should be executed on.</param>
		''' <param name="transaction">The <see cref="SqlTransaction"/> the <see cref="SqlCommand"/> should be executed with.</param>
		''' <param name="commandType">The command type.</param>
		''' <param name="commandText">The name of the stored procedure to execute.</param>
		''' <param name="parameters">The parameters of the stored procedure.</param>
		''' <returns>An initialized <see cref="SqlCommand"/> instance.</returns>
		Private Shared Function CreateCommand(connection As SqlConnection, transaction As SqlTransaction, commandType As CommandType, commandText As String, ParamArray parameters As SqlParameter()) As SqlCommand
			Dim command As New SqlCommand()
			command.Connection = connection
			command.CommandText = commandText
			command.CommandType = commandType
			command.Transaction = transaction

			If parameters IsNot Nothing Then
				' Append each parameter to the command
				For Each parameter As SqlParameter In parameters
					parameter.Value = NullHandler.HandleAppNull(parameter.Value, DBNull.Value) 
					command.Parameters.Add(parameter)
				Next
			End If

			Return command
		End Function
		#End Region

		#Region "CreateDataX"
        Private Shared Function CreateDataSet(ByVal command As SqlCommand) As DataSet
            Using dataAdapter As New SqlDataAdapter(command)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet)
                Return dataSet
            End Using
        End Function

        Private Shared Function CreateDataTable(ByVal command As SqlCommand) As DataTable
            Using dataAdapter As New SqlDataAdapter(command)
                Dim dataTable As New DataTable()
                dataAdapter.Fill(dataTable)
                Return dataTable
            End Using
        End Function
		#End Region
		#End Region

    End Class

'End NameSpace
