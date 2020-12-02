Imports System
Imports System.Data
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.ServiceModel
'Imports System.ComponentModel

'Namespace Vb_BuiltIn_WcfServiceApp 
	'<DataObject(true)> _
	Partial Public Class Test_detailService 
		Implements ITest_detailService 
		Protected Sub New() 
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _
		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		Public Sub Insert(master_id As Int32, id As String, description As String, qty As Int32, amt As Double) _
		 Implements ITest_detailService.Insert
			Test_detailController.Insert( _
				master_id, _
				id, _
				description, _
				qty, _
				amt _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _
		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		Public Sub InsertInfo(ByVal info As Test_detailContract) _
		 Implements ITest_detailService.InsertInfo
			Test_detailController.InsertInfo( _
				info.ToTest_detailInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _
		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		Public Sub Update(master_id As Int32, id As String, description As String, qty As Int32, amt As Double) _
		 Implements ITest_detailService.Update
			Test_detailController.Update( _
				master_id, _
				id, _
				description, _
				qty, _
				amt _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _
		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		Public Sub UpdateInfo(ByVal info As Test_detailContract) _
		 Implements ITest_detailService.UpdateInfo
			Test_detailController.UpdateInfo( _
				info.ToTest_detailInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		Public Sub Delete(master_id As Int32, id As String) _
		 Implements ITest_detailService.Delete
			Test_detailController.Delete( _
				master_id, _
				id _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		Public Sub DeleteInfo(ByVal info As Test_detailContract) _
		 Implements ITest_detailService.DeleteInfo
			Test_detailController.DeleteInfo( _
				info.ToTest_detailInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		Public Sub DeleteByMaster_id(master_id As Int32) _
		 Implements ITest_detailService.DeleteByMaster_id
			Test_detailController.DeleteByMaster_id( _
				master_id _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		Public Sub DeleteInfoByMaster_id(ByVal info As Test_detailContract) _
		 Implements ITest_detailService.DeleteInfoByMaster_id
			Test_detailController.DeleteInfoByMaster_id( _
				info.ToTest_detailInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		Public Function SelectDS(master_id As Int32, id As String) As DataSet _
		 Implements ITest_detailService.SelectDS
			Return Test_detailController.SelectDS _
			( _
				master_id, _
				id _
			)
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		Public Function SelectInfo(ByVal info As Test_detailContract) As BindingListView(Of Test_detailContract) _
		 Implements ITest_detailService.SelectInfo
				Return Test_detailController.SelectInfo(info.ToTest_detailInfo()).ToBindingListViewOfContract()
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		Public Function SelectDSAll() As DataSet  _
		 Implements ITest_detailService.SelectDSAll
			Return Test_detailController.SelectDSAll()
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		Public Function SelectInfoAll() As BindingListView(Of Test_detailContract)  _
		 Implements ITest_detailService.SelectInfoAll
			Return Test_detailController.SelectInfoAll().ToBindingListViewOfContract()
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		Public Function SelectDSByMaster_id(master_id As Int32) As DataSet _
		 Implements ITest_detailService.SelectDSByMaster_id
			Return Test_detailController.SelectDSByMaster_id( _
				master_id _
			)
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		Public Function SelectInfoByMaster_id(ByVal info As Test_detailContract) As BindingListView(Of Test_detailContract) _
		 Implements ITest_detailService.SelectInfoByMaster_id
			Return Test_detailController.SelectInfoByMaster_id(info.ToTest_detailInfo()).ToBindingListViewOfContract()
		End Function
	End Class
'End Namespace
