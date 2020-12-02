Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

'NameSpace Vb_BuiltIn_WcfServiceApp

Class SortComparer(Of T)
    Implements IComparer(Of T)

    Private m_SortCollection As ListSortDescriptionCollection = Nothing
    Private m_PropDesc As PropertyDescriptor = Nothing
    Private m_Direction As ListSortDirection = ListSortDirection.Ascending

    Public Sub New(ByVal propDesc As PropertyDescriptor, ByVal direction As ListSortDirection)
        m_PropDesc = propDesc
        m_Direction = direction
    End Sub

    Public Sub New(ByVal sortCollection As ListSortDescriptionCollection)
        m_SortCollection = sortCollection
    End Sub

    Function Compare(ByVal x As T, ByVal y As T) As Integer Implements IComparer(Of T).Compare

        If (Not m_PropDesc Is Nothing) Then ' Simple sort

            Dim xValue As Object = m_PropDesc.GetValue(x)
            Dim yValue As Object = m_PropDesc.GetValue(y)
            Return CompareValues(xValue, yValue, m_Direction)

        ElseIf ((Not m_SortCollection Is Nothing) And m_SortCollection.Count > 0) Then

            Return RecursiveCompareInternal(x, y, 0)

        Else
            Return 0
        End If
    End Function

    Private Function CompareValues(ByVal xValue As Object, ByVal yValue As Object, ByVal direction As ListSortDirection) As Integer

        Dim retValue As Integer = 0

        If (TypeOf (xValue) Is IComparable) Then ' Can ask the x value
            retValue = (CType(xValue, IComparable)).CompareTo(yValue)
        ElseIf (TypeOf (yValue) Is IComparable) Then 'Can ask the y value
            retValue = (CType(yValue, IComparable)).CompareTo(xValue)
        ElseIf (Not xValue.Equals(yValue)) Then ' not comparable, compare String representations
            retValue = xValue.ToString().CompareTo(yValue.ToString())
        End If

        If (direction = ListSortDirection.Ascending) Then
            Return retValue
        Else
            Return retValue * -1
        End If
    End Function

    Private Function RecursiveCompareInternal(ByVal x As T, ByVal y As T, ByVal index As Integer) As Integer

        If (index >= m_SortCollection.Count) Then
            Return 0 ' termination condition
        End If

        Dim listSortDesc As ListSortDescription = m_SortCollection(index)
        Dim xValue As Object = listSortDesc.PropertyDescriptor.GetValue(x)
        Dim yValue As Object = listSortDesc.PropertyDescriptor.GetValue(y)

        Dim retValue As Integer = CompareValues(xValue, yValue, listSortDesc.SortDirection)
        If (retValue = 0) Then

            Return RecursiveCompareInternal(x, y, ++index)

        Else

            Return retValue
        End If
    End Function

End Class

'End NameSpace
