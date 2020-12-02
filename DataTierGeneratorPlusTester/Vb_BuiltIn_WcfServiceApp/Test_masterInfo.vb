Imports System
Imports System.Collections
Imports System.ComponentModel 

'Namespace Vb_BuiltIn_WcfServiceApp 

	''' <summary>
	''' Class that stores table fields.
	''' </summary>
	Public Class Test_masterInfo 

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

		''' <summary>
		''' id  
		''' </summary>
		<DataObjectFieldAttribute(True, False, False)> _
		Public Property id As Int32
			Get 
				Return _id 
			End Get
			Set(ByVal Value As Int32) 
				_id = Value
			End Set
		End Property

		''' <summary>
		''' description  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property description As String
			Get 
				Return _description 
			End Get
			Set(ByVal Value As String) 
				_description = Value
			End Set
		End Property

		''' <summary>
		''' notes  
		''' </summary>
		<DataObjectFieldAttribute(False, False, True)> _
		Public Property notes As String
			Get 
				Return _notes 
			End Get
			Set(ByVal Value As String) 
				_notes = Value
			End Set
		End Property

		''' <summary>
		''' someint  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property someint As Int32
			Get 
				Return _someint 
			End Get
			Set(ByVal Value As Int32) 
				_someint = Value
			End Set
		End Property

		''' <summary>
		''' someint_nullable  
		''' </summary>
		<DataObjectFieldAttribute(False, False, True)> _
		Public Property someint_nullable As Int32?
			Get 
				Return _someint_nullable 
			End Get
			Set(ByVal Value As Int32?) 
				_someint_nullable = Value
			End Set
		End Property

		''' <summary>
		''' somedate  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property somedate As DateTime
			Get 
				Return _somedate 
			End Get
			Set(ByVal Value As DateTime) 
				_somedate = Value
			End Set
		End Property

		''' <summary>
		''' somedate_nullable  
		''' </summary>
		<DataObjectFieldAttribute(False, False, True)> _
		Public Property somedate_nullable As DateTime?
			Get 
				Return _somedate_nullable 
			End Get
			Set(ByVal Value As DateTime?) 
				_somedate_nullable = Value
			End Set
		End Property

		''' <summary>
		''' somefloat  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property somefloat As Double
			Get 
				Return _somefloat 
			End Get
			Set(ByVal Value As Double) 
				_somefloat = Value
			End Set
		End Property

		''' <summary>
		''' somefloat_nullable  
		''' </summary>
		<DataObjectFieldAttribute(False, False, True)> _
		Public Property somefloat_nullable As Double?
			Get 
				Return _somefloat_nullable 
			End Get
			Set(ByVal Value As Double?) 
				_somefloat_nullable = Value
			End Set
		End Property

		''' <summary>
		''' somebool  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property somebool As Boolean
			Get 
				Return _somebool 
			End Get
			Set(ByVal Value As Boolean) 
				_somebool = Value
			End Set
		End Property

		''' <summary>
		''' somebool_nullable  
		''' </summary>
		<DataObjectFieldAttribute(False, False, True)> _
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
