Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.ComponentModel

'Namespace VB_BuiltIn 
	<DataObject(true)> _
	Partial Public Class Test_detailController 
		Protected Sub New() 
		End Sub

		'Public enums for column positions on the [dbo].[test_detail] table.
		Public Enum ColumnIndex As Integer
		Master_id = 0
		Id = 1
		Description = 2
		Qty = 3
		Amt = 4
		End Enum

		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _
		Public Shared Sub Insert(master_id As Int32, id As String, description As String, qty As Int32, amt As Double)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)), _
				New SqlParameter("@qty", NullHandler.HandleAppNull(qty, DBNull.Value)), _
				New SqlParameter("@amt", NullHandler.HandleAppNull(amt, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _
		Public Shared Sub InsertInfo(ByVal info As Test_detailInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailInsert]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)), _
				New SqlParameter("@qty", NullHandler.HandleAppNull(info.qty, DBNull.Value)), _
				New SqlParameter("@amt", NullHandler.HandleAppNull(info.amt, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _
		Public Shared Sub Update(master_id As Int32, id As String, description As String, qty As Int32, amt As Double)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(description, DBNull.Value)), _
				New SqlParameter("@qty", NullHandler.HandleAppNull(qty, DBNull.Value)), _
				New SqlParameter("@amt", NullHandler.HandleAppNull(amt, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _
		Public Shared Sub UpdateInfo(ByVal info As Test_detailInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailUpdate]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)), _
				New SqlParameter("@description", NullHandler.HandleAppNull(info.description, DBNull.Value)), _
				New SqlParameter("@qty", NullHandler.HandleAppNull(info.qty, DBNull.Value)), _
				New SqlParameter("@amt", NullHandler.HandleAppNull(info.amt, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		Public Shared Sub Delete(master_id As Int32, id As String)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _
		Public Shared Sub DeleteInfo(ByVal info As Test_detailInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDelete]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(info.id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		Public Shared Sub DeleteByMaster_id(master_id As Int32)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		Public Shared Sub DeleteInfoByMaster_id(ByVal info As Test_detailInfo)
			DatabaseUtility.ExecuteNonQuery(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailDeleteByMaster_id]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(info.master_id, DBNull.Value)) _
			)
		End Sub

		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDR(master_id As Int32, id As String) As SqlDataReader 
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDS(master_id As Int32, id As String) As DataSet 
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelect]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)), _
				New SqlParameter("@id", NullHandler.HandleAppNull(id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		Public Shared Function SelectInfo(ByVal info As Test_detailInfo) As BindingListView(Of Test_detailInfo) 
			Using dr As SqlDataReader = SelectDR (info.master_id, info.id) 
				Return LoadListDR(dr)
			End Using
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDRAll() As SqlDataReader  
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]")
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDSAll() As DataSet  
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectAll]")
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		Public Shared Function SelectInfoAll() As BindingListView(Of Test_detailInfo)  
			Using dr As SqlDataReader = SelectDRAll() 
				Return LoadListDR(dr)
			End Using
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDRByMaster_id(master_id As Int32) As SqlDataReader 
			return DatabaseUtility.ExecuteReader(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		Public Shared Function SelectDSByMaster_id(master_id As Int32) As DataSet 
			return DatabaseUtility.ExecuteDataSet(DatabaseUtility.GetConnection(), DatabaseUtility.TransactionObject, "[dbo].[test_detailSelectByMaster_id]", _
				New SqlParameter("@master_id", NullHandler.HandleAppNull(master_id, DBNull.Value)) _
			)
		End Function

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		Public Shared Function SelectInfoByMaster_id(ByVal info As Test_detailInfo) As BindingListView(Of Test_detailInfo) 
			Using dr As SqlDataReader = SelectDRByMaster_id(info.master_id) 
				Return LoadListDR(dr)
			End Using
		End Function

		''' <summary>
		''' Loads all records from the [dbo].[test_detail] table into List of Test_detailInfo.
		''' </summary>
		Protected Shared Function LoadListDR(ByVal dr As SqlDataReader) As BindingListView(Of Test_detailInfo)  
			Dim infoList As BindingListView(Of Test_detailInfo) = New BindingListView(Of Test_detailInfo)
			While (dr.Read())
				infoList.Add _
					( _
					New Test_detailInfo _
						( _
						NullHandler.HandleDbNull(dr("master_id")), _
						NullHandler.HandleDbNull(dr("id")), _
						NullHandler.HandleDbNull(dr("description")), _
						NullHandler.HandleDbNull(dr("qty")), _
						NullHandler.HandleDbNull(dr("amt")) _
						) _
					)
			End While
			Return infoList
		End Function
	End Class
'End Namespace
