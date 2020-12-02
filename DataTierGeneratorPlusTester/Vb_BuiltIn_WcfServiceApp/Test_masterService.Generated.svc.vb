Imports System
Imports System.Data
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.ServiceModel
'Imports System.ComponentModel

'Namespace Vb_BuiltIn_WcfServiceApp 
	'<DataObject(true)> _
	Partial Public Class Test_masterService 
		Implements ITest_masterService 
		Protected Sub New() 
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Insert, False)> _
		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		Public Sub Insert(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?) _
		 Implements ITest_masterService.Insert
			Test_masterController.Insert( _
				id, _
				description, _
				notes, _
				someint, _
				someint_nullable, _
				somedate, _
				somedate_nullable, _
				somefloat, _
				somefloat_nullable, _
				somebool, _
				somebool_nullable _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Insert, True)> _
		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		Public Sub InsertInfo(ByVal info As Test_masterContract) _
		 Implements ITest_masterService.InsertInfo
			Test_masterController.InsertInfo( _
				info.ToTest_masterInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Update, False)> _
		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		Public Sub Update(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?) _
		 Implements ITest_masterService.Update
			Test_masterController.Update( _
				id, _
				description, _
				notes, _
				someint, _
				someint_nullable, _
				somedate, _
				somedate_nullable, _
				somefloat, _
				somefloat_nullable, _
				somebool, _
				somebool_nullable _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Update, True)> _
		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		Public Sub UpdateInfo(ByVal info As Test_masterContract) _
		 Implements ITest_masterService.UpdateInfo
			Test_masterController.UpdateInfo( _
				info.ToTest_masterInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, False)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		Public Sub Delete(id As Int32) _
		 Implements ITest_masterService.Delete
			Test_masterController.Delete( _
				id _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Delete, True)> _
		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		Public Sub DeleteInfo(ByVal info As Test_masterContract) _
		 Implements ITest_masterService.DeleteInfo
			Test_masterController.DeleteInfo( _
				info.ToTest_masterInfo() _
			)
		End Sub

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		Public Function SelectDS(id As Int32) As DataSet _
		 Implements ITest_masterService.SelectDS
			Return Test_masterController.SelectDS _
			( _
				id _
			)
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		Public Function SelectInfo(ByVal info As Test_masterContract) As BindingListView(Of Test_masterContract) _
		 Implements ITest_masterService.SelectInfo
				Return Test_masterController.SelectInfo(info.ToTest_masterInfo()).ToBindingListViewOfContract()
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, False)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		Public Function SelectDSAll() As DataSet  _
		 Implements ITest_masterService.SelectDSAll
			Return Test_masterController.SelectDSAll()
		End Function

		'<DataObjectMethodAttribute(DataObjectMethodType.Select, True)> _
		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		Public Function SelectInfoAll() As BindingListView(Of Test_masterContract)  _
		 Implements ITest_masterService.SelectInfoAll
			Return Test_masterController.SelectInfoAll().ToBindingListViewOfContract()
		End Function
	End Class
'End Namespace
