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
	Public Interface ITest_masterService 

		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Sub Insert(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?)

		''' <summary>
		''' Inserts a record into the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Sub InsertInfo(ByVal info As Test_masterContract)

		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Sub Update(id As Int32, description As String, notes As String, someint As Int32, someint_nullable As Int32?, somedate As DateTime, somedate_nullable As DateTime?, somefloat As Double, somefloat_nullable As Double?, somebool As Boolean, somebool_nullable As Boolean?)

		''' <summary>
		''' Updates a record in the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Sub UpdateInfo(ByVal info As Test_masterContract)

		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		<OperationContract()> _
		Sub Delete(id As Int32)

		''' <summary>
		''' Deletes a record from the [dbo].[test_master] table by a composite primary key.
		''' </summary>
		<OperationContract()> _
		Sub DeleteInfo(ByVal info As Test_masterContract)

		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Function SelectDS(id As Int32) As DataSet 

		''' <summary>
		''' Selects a single record from the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Function SelectInfo(ByVal info As Test_masterContract) As BindingListView(Of Test_masterContract) 

		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Function SelectDSAll() As DataSet  

		''' <summary>
		''' Selects all records from the [dbo].[test_master] table.
		''' </summary>
		<OperationContract()> _
		Function SelectInfoAll() As BindingListView(Of Test_masterContract)  
	End Interface

	''' <summary>
	''' Class that stores table fields.
	''' </summary>
	<DataContract()> _
	Public Class Test_masterContract 
		''' <summary>
		''' Default constructor.  
		''' </summary>
		Public Sub New() 
		End Sub 

		''' <summary>
		''' Constructor with values.  
		''' </summary>
		Public Sub New( _
			id As Int32, _
			description As String, _
			notes As String, _
			someint As Int32, _
			someint_nullable As Int32?, _
			somedate As DateTime, _
			somedate_nullable As DateTime?, _
			somefloat As Double, _
			somefloat_nullable As Double?, _
			somebool As Boolean, _
			somebool_nullable As Boolean? _
		)
			_id = id
			_description = description
			_notes = notes
			_someint = someint
			_someint_nullable = someint_nullable
			_somedate = somedate
			_somedate_nullable = somedate_nullable
			_somefloat = somefloat
			_somefloat_nullable = somefloat_nullable
			_somebool = somebool
			_somebool_nullable = somebool_nullable
		End Sub

		' Private variables for columns in the test_master table.
		Private  _id As Int32
		Private  _description As String
		Private  _notes As String
		Private  _someint As Int32
		Private  _someint_nullable As Int32?
		Private  _somedate As DateTime
		Private  _somedate_nullable As DateTime?
		Private  _somefloat As Double
		Private  _somefloat_nullable As Double?
		Private  _somebool As Boolean
		Private  _somebool_nullable As Boolean?

		' Public properties for columns in the test_master table.

		'<DataObjectFieldAttribute(True, False, False)> _
		''' <summary>
		''' id  
		''' </summary>
		<DataMember()> _
		Public Property id As Int32
			Get 
				Return _id 
			End Get
			Set(ByVal Value As Int32) 
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

		'<DataObjectFieldAttribute(False, False, True)> _
		''' <summary>
		''' notes  
		''' </summary>
		<DataMember()> _
		Public Property notes As String
			Get 
				Return _notes 
			End Get
			Set(ByVal Value As String) 
				_notes = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' someint  
		''' </summary>
		<DataMember()> _
		Public Property someint As Int32
			Get 
				Return _someint 
			End Get
			Set(ByVal Value As Int32) 
				_someint = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, True)> _
		''' <summary>
		''' someint_nullable  
		''' </summary>
		<DataMember()> _
		Public Property someint_nullable As Int32?
			Get 
				Return _someint_nullable 
			End Get
			Set(ByVal Value As Int32?) 
				_someint_nullable = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' somedate  
		''' </summary>
		<DataMember()> _
		Public Property somedate As DateTime
			Get 
				Return _somedate 
			End Get
			Set(ByVal Value As DateTime) 
				_somedate = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, True)> _
		''' <summary>
		''' somedate_nullable  
		''' </summary>
		<DataMember()> _
		Public Property somedate_nullable As DateTime?
			Get 
				Return _somedate_nullable 
			End Get
			Set(ByVal Value As DateTime?) 
				_somedate_nullable = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' somefloat  
		''' </summary>
		<DataMember()> _
		Public Property somefloat As Double
			Get 
				Return _somefloat 
			End Get
			Set(ByVal Value As Double) 
				_somefloat = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, True)> _
		''' <summary>
		''' somefloat_nullable  
		''' </summary>
		<DataMember()> _
		Public Property somefloat_nullable As Double?
			Get 
				Return _somefloat_nullable 
			End Get
			Set(ByVal Value As Double?) 
				_somefloat_nullable = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, False)> _
		''' <summary>
		''' somebool  
		''' </summary>
		<DataMember()> _
		Public Property somebool As Boolean
			Get 
				Return _somebool 
			End Get
			Set(ByVal Value As Boolean) 
				_somebool = Value
			End Set
		End Property

		'<DataObjectFieldAttribute(False, False, True)> _
		''' <summary>
		''' somebool_nullable  
		''' </summary>
		<DataMember()> _
		Public Property somebool_nullable As Boolean?
			Get 
				Return _somebool_nullable 
			End Get
			Set(ByVal Value As Boolean?) 
				_somebool_nullable = Value
			End Set
		End Property
	End Class
'End Namespace
