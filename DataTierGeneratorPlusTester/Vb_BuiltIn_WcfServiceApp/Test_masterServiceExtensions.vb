Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.ServiceModel

'Namespace Vb_BuiltIn_WcfServiceApp 

	Friend Module Test_masterServiceExtensions 

		''' <summary>
		''' Converts from List of Test_masterInfo to List of Test_masterContract.
		''' </summary>
		<Extension()> _
		Friend Function ToBindingListViewOfContract(ByVal infoList As BindingListView(Of Test_masterInfo)) As BindingListView(Of Test_masterContract)  
			Dim returnValue As New BindingListView(Of Test_masterContract) 
			For Each info As Test_masterInfo In infoList
				returnValue.Add _
					( _
					info.ToTest_masterContract() _
					)
			Next
			Return returnValue
		End Function

		''' <summary>
		'''  Converts from Test_masterInfo to Test_masterContract.
		''' </summary>
		<Extension()> _
		Friend Function ToTest_masterContract(ByVal info As Test_masterInfo) As Test_masterContract
			Return New Test_masterContract( _
				info.id, _
				info.description, _
				info.notes, _
				info.someint, _
				info.someint_nullable, _
				info.somedate, _
				info.somedate_nullable, _
				info.somefloat, _
				info.somefloat_nullable, _
				info.somebool, _
				info.somebool_nullable _
			)
		End Function

		''' <summary>
		'''  Converts from Test_masterContract to Test_masterInfo.
		''' </summary>
		<Extension()> _
		Friend Function ToTest_masterInfo(ByVal info As Test_masterContract) As Test_masterInfo
			Return New Test_masterInfo( _
				info.id, _
				info.description, _
				info.notes, _
				info.someint, _
				info.someint_nullable, _
				info.somedate, _
				info.somedate_nullable, _
				info.somefloat, _
				info.somefloat_nullable, _
				info.somebool, _
				info.somebool_nullable _
			)
		End Function
	End Module
'End Namespace
