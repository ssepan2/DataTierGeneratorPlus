Imports System
Imports System.Data
Imports System.Runtime.Serialization
Imports System.ServiceModel
'Imports System.ComponentModel 

'Namespace Vb_BuiltIn_WcfServiceApp 
	''' <summary>
	''' Class that defines the table operations.
	''' </summary>
	<ServiceContract()> _
	Public Interface ITest_detailService 

		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Sub Insert(master_id As Int32, id As String, description As String, qty As Int32, amt As Double)

		''' <summary>
		''' Inserts a record into the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Sub InsertInfo(ByVal info As Test_detailContract)

		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Sub Update(master_id As Int32, id As String, description As String, qty As Int32, amt As Double)

		''' <summary>
		''' Updates a record in the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Sub UpdateInfo(ByVal info As Test_detailContract)

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		<OperationContract()> _
		Sub Delete(master_id As Int32, id As String)

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a composite primary key.
		''' </summary>
		<OperationContract()> _
		Sub DeleteInfo(ByVal info As Test_detailContract)

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<OperationContract()> _
		Sub DeleteByMaster_id(master_id As Int32) 

		''' <summary>
		''' Deletes a record from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<OperationContract()> _
		Sub DeleteInfoByMaster_id(ByVal info As Test_detailContract)

		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Function SelectDS(master_id As Int32, id As String) As DataSet 

		''' <summary>
		''' Selects a single record from the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Function SelectInfo(ByVal info As Test_detailContract) As BindingListView(Of Test_detailContract) 

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Function SelectDSAll() As DataSet  

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table.
		''' </summary>
		<OperationContract()> _
		Function SelectInfoAll() As BindingListView(Of Test_detailContract)  

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<OperationContract()> _
		Function SelectDSByMaster_id(master_id As Int32) As DataSet 

		''' <summary>
		''' Selects all records from the [dbo].[test_detail] table by a foreign key.
		''' </summary>
		<OperationContract()> _
		Function SelectInfoByMaster_id(ByVal info As Test_detailContract) As BindingListView(Of Test_detailContract) 
	End Interface

	''' <summary>
	''' Class that stores table fields.
	''' </summary>
	<DataContract()> _
	Public Class Test_detailContract 
		''' <summary>
		''' Default constructor.  
		''' </summary>
		Public Sub New() 
		End Sub 

		''' <summary>
		''' Constructor with values.  
		''' </summary>
		Public Sub New( _
			master_id As Int32, _
			id As String, _
			description As String, _
			qty As Int32, _
			amt As Double _
		)
			_master_id = master_id
			_id = id
			_description = description
			_qty = qty
			_amt = amt
		End Sub

		' Private variables for columns in the test_detail table.
		Private  _master_id As Int32
		Private  _id As String
		Private  _description As String
		Private  _qty As Int32
		Private  _amt As Double

		' Public properties for columns in the test_detail table.

		'<DataObjectFieldAttribute(True, False, False)> _
		''' <summary>
		''' master_id  
		''' </summary>
		<DataMember()> _
		Public Property master_id As Int32
			Get 
				Return _master_id 
			End Get
			Set(ByVal Value As Int32) 
				_master_id = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(True, False, False)> _
		''' <summary>
		''' id  
		''' </summary>
		<DataMember()> _
		Public Property id As String
			Get 
				Return _id 
			End Get
			Set(ByVal Value As String) 
				_id = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' description  
		''' </summary>
		<DataMember()> _
		Public Property description As String
			Get 
				Return _description 
			End Get
			Set(ByVal Value As String) 
				_description = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' qty  
		''' </summary>
		<DataMember()> _
		Public Property qty As Int32
			Get 
				Return _qty 
			End Get
			Set(ByVal Value As Int32) 
				_qty = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' amt  
		''' </summary>
		<DataMember()> _
		Public Property amt As Double
			Get 
				Return _amt 
			End Get
			Set(ByVal Value As Double) 
				_amt = Value
			End Set
		End Property
	End Class
'End Namespace
