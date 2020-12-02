Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Imports VB_BuiltIn.Test_masterService

'Namespace VB_BuiltIn 

	Friend Module Test_masterServiceClientExtensions 

		''' <summary>
		''' Converts from Array of Test_masterContract to List of Test_masterContract.
		''' </summary>
		<Extension()> _
		Friend Function ToBindingListViewOfContract(ByVal infoList As Test_masterContract()) As BindingListView(Of Test_masterContract)  
			Dim returnValue As New BindingListView(Of Test_masterContract) 
			For Each info As Test_masterContract In infoList
				returnValue.Add _
					( _
					info _
					)
			Next
			Return returnValue
		End Function
	End Module
'End Namespace
