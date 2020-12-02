Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

'Namespace Vb_BuiltIn_WcfServiceApp 
	<DataObject(true)> _
	Partial Public Class Test_masterController 
		Protected Sub New() 
		End Sub

		'Public enums for column positions on the [dbo].[test_master] table.
		Public Enum ColumnIndex As Integer
		Id = 0
		Description = 1
		Notes = 2
		Someint = 3
		Someint_nullable = 4
		Somedate = 5
		Somedate_nullable = 6
		Somefloat = 7
		Somefloat_nullable = 8
		Somebool = 9
		Somebool_nullable = 10
		End Enum

		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _
		Public Shared Sub Insert(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)), _
				New SqlParameter("@notes", NullHandler.HandleAppNull(notes, DBNull.Value)), _
				New SqlParameter("@someint", NullHandler.HandleAppNull(someint, DBNull.Value)), _
				New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(someint_nullable, DBNull.Value)), _
				New SqlParameter("@somedate", NullHandler.HandleAppNull(somedate, DBNull.Value)), _
				New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(somedate_nullable, DBNull.Value)), _
				New SqlParameter("@somefloat", NullHandler.HandleAppNull(somefloat, DBNull.Value)), _
				New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(somefloat_nullable, DBNull.Value)), _
				New SqlParameter("@somebool", NullHandler.HandleAppNull(somebool, DBNull.Value)), _
				New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(somebool_nullable, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _
		Public Shared Sub InsertInfo(ByVal info As Test_masterInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterInsert]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)), _
				New SqlParameter("@notes", NullHandler.HandleAppNull(info.notes, DBNull.Value)), _
				New SqlParameter("@someint", NullHandler.HandleAppNull(info.someint, DBNull.Value)), _
				New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(info.someint_nullable, DBNull.Value)), _
				New SqlParameter("@somedate", NullHandler.HandleAppNull(info.somedate, DBNull.Value)), _
				New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(info.somedate_nullable, DBNull.Value)), _
				New SqlParameter("@somefloat", NullHandler.HandleAppNull(info.somefloat, DBNull.Value)), _
				New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(info.somefloat_nullable, DBNull.Value)), _
				New SqlParameter("@somebool", NullHandler.HandleAppNull(info.somebool, DBNull.Value)), _
				New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(info.somebool_nullable, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _
		Public Shared Sub Update(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)), _
				New SqlParameter("@notes", NullHandler.HandleAppNull(notes, DBNull.Value)), _
				New SqlParameter("@someint", NullHandler.HandleAppNull(someint, DBNull.Value)), _
				New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(someint_nullable, DBNull.Value)), _
				New SqlParameter("@somedate", NullHandler.HandleAppNull(somedate, DBNull.Value)), _
				New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(somedate_nullable, DBNull.Value)), _
				New SqlParameter("@somefloat", NullHandler.HandleAppNull(somefloat, DBNull.Value)), _
				New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(somefloat_nullable, DBNull.Value)), _
				New SqlParameter("@somebool", NullHandler.HandleAppNull(somebool, DBNull.Value)), _
				New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(somebool_nullable, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _
		Public Shared Sub UpdateInfo(ByVal info As Test_masterInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterUpdate]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)), _
				New SqlParameter("@notes", NullHandler.HandleAppNull(info.notes, DBNull.Value)), _
				New SqlParameter("@someint", NullHandler.HandleAppNull(info.someint, DBNull.Value)), _
				New SqlParameter("@someint_nullable", NullHandler.HandleAppNull(info.someint_nullable, DBNull.Value)), _
				New SqlParameter("@somedate", NullHandler.HandleAppNull(info.somedate, DBNull.Value)), _
				New SqlParameter("@somedate_nullable", NullHandler.HandleAppNull(info.somedate_nullable, DBNull.Value)), _
				New SqlParameter("@somefloat", NullHandler.HandleAppNull(info.somefloat, DBNull.Value)), _
				New SqlParameter("@somefloat_nullable", NullHandler.HandleAppNull(info.somefloat_nullable, DBNull.Value)), _
				New SqlParameter("@somebool", NullHandler.HandleAppNull(info.somebool, DBNull.Value)), _
				New SqlParameter("@somebool_nullable", NullHandler.HandleAppNull(info.somebool_nullable, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		Public Shared Sub Delete(id As Int32)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _
		Public Shared Sub DeleteInfo(ByVal info As Test_masterInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterDelete]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDR(id As Int32) As SqlDataReader 
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDS(id As Int32) As DataSet 
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelect]", _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		Public Shared Function SelectInfo(ByVal info As Test_masterInfo) As BindingListView(Of Test_masterInfo) 
			Using dr As SqlDataReader = SelectDR (info.id) 
				Return LoadListDR(dr)
			End Using
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDRAll() As SqlDataReader  
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]")
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDSAll() As DataSet  
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_masterSelectAll]")
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		Public Shared Function SelectInfoAll() As BindingListView(Of Test_masterInfo)  
			Using dr As SqlDataReader = SelectDRAll() 
				Return LoadListDR(dr)
			End Using
		End Function

		''' <summary>
		''' Loads all records from the [dbo].[test_master] table into List of Test_masterInfo.
		''' </summary>
		Protected Shared Function LoadListDR(ByVal dr As SqlDataReader) As BindingListView(Of Test_masterInfo)  
			Dim infoList As BindingListView(Of Test_masterInfo) = New BindingListView(Of Test_masterInfo)
			While (dr.Read())
				infoList.Add _
					( _
					New Test_masterInfo _
						( _
						NullHandler.HandleDbNull(dr("id")), _
						NullHandler.HandleDbNull(dr("description")), _
						NullHandler.HandleDbNull(dr("notes")), _
						NullHandler.HandleDbNull(dr("someint")), _
						NullHandler.HandleDbNull(dr("someint_nullable")), _
						NullHandler.HandleDbNull(dr("somedate")), _
						NullHandler.HandleDbNull(dr("somedate_nullable")), _
						NullHandler.HandleDbNull(dr("somefloat")), _
						NullHandler.HandleDbNull(dr("somefloat_nullable")), _
						NullHandler.HandleDbNull(dr("somebool")), _
						NullHandler.HandleDbNull(dr("somebool_nullable")) _
						) _
					)
			End While
			Return infoList
		End Function
	End Class
'End Namespace
