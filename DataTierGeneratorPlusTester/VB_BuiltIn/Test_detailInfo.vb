Imports System
Imports System.Collections
Imports System.ComponentModel 

'Namespace VB_BuiltIn 

	''' <summary>
	''' Class that stores table fields.
	''' </summary>
	Public Class Test_detailInfo 

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

		''' <summary>
		''' master_id  
		''' </summary>
		<DataObjectFieldAttribute(True, False, False)> _
		Public Property master_id As Int32
			Get 
				Return _master_id 
			End Get
			Set(ByVal Value As Int32) 
				_master_id = Value
			End Set
		End Property

		''' <summary>
		''' id  
		''' </summary>
		<DataObjectFieldAttribute(True, False, False)> _
		Public Property id As String
			Get 
				Return _id 
			End Get
			Set(ByVal Value As String) 
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
		''' qty  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
		Public Property qty As Int32
			Get 
				Return _qty 
			End Get
			Set(ByVal Value As Int32) 
				_qty = Value
			End Set
		End Property

		''' <summary>
		''' amt  
		''' </summary>
		<DataObjectFieldAttribute(False, False, False)> _
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
