Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.ServiceModel

'Namespace Vb_BuiltIn_WcfServiceApp 

	Friend Module Test_detailServiceExtensions 

		''' <summary>
		''' Converts from List of Test_detailInfo to List of Test_detailContract.
		''' </summary>
		<Extension()> _
		Friend Function ToBindingListViewOfContract(ByVal infoList As BindingListView(Of Test_detailInfo)) As BindingListView(Of Test_detailContract)  
			Dim returnValue As New BindingListView(Of Test_detailContract) 
			For Each info As Test_detailInfo In infoList
				returnValue.Add _
					( _
					info.ToTest_detailContract() _
					)
			Next
			Return returnValue
		End Function

		''' <summary>
		'''  Converts from Test_detailInfo to Test_detailContract.
		''' </summary>
		<Extension()> _
		Friend Function ToTest_detailContract(ByVal info As Test_detailInfo) As Test_detailContract
			Return New Test_detailContract( _
				info.master_id, _
				info.id, _
				info.description, _
				info.qty, _
				info.amt _
			)
		End Function

		''' <summary>
		'''  Converts from Test_detailContract to Test_detailInfo.
		''' </summary>
		<Extension()> _
		Friend Function ToTest_detailInfo(ByVal info As Test_detailContract) As Test_detailInfo
			Return New Test_detailInfo( _
				info.master_id, _
				info.id, _
				info.description, _
				info.qty, _
				info.amt _
			)
		End Function
	End Module
'End Namespace
