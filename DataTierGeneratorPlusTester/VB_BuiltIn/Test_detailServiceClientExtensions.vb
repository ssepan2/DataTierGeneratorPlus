Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports VB_BuiltIn.Test_detailService

'Namespace VB_BuiltIn 

	Friend Module Test_detailServiceClientExtensions 

		''' <summary>
		''' Converts from Array of Test_detailContract to List of Test_detailContract.
		''' </summary>
		<Extension()> _
		Friend Function ToBindingListViewOfContract(ByVal infoList As Test_detailContract()) As BindingListView(Of Test_detailContract)  
			Dim returnValue As New BindingListView(Of Test_detailContract) 
			For Each info As Test_detailContract In infoList
				returnValue.Add _
					( _
					info _
					)
			Next
			Return returnValue
		End Function
	End Module
'End Namespace
