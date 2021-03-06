Imports System
Imports System.Collections.Generic
Imports System.ComponentModel

'NameSpace VB_BuiltIn

Public Class BindingListView(Of T)
    Inherits BindingList(Of T)
    Implements IRaiseItemChangedEvents, IBindingListView


    Private _isSorted As Boolean = False
    Private _isFiltered As Boolean = False
    Private _filterString As String = Nothing
    Private _filterPredicate As Predicate(Of T) = Nothing
    Private _sortDirection As ListSortDirection = ListSortDirection.Ascending
    Private _sortProperty As PropertyDescriptor = Nothing
    Private _sortDescriptions As ListSortDescriptionCollection = New ListSortDescriptionCollection()
    Private _originalCollection As List(Of T) = New List(Of T)()

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal list As List(Of T))
        MyBase.New(list)
    End Sub

    Protected Overrides ReadOnly Property SupportsSearchingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides Function FindCore(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer

        ' Simple iteration:

        For i As Integer = 0 To Count - 1
            Dim item As T = Me.Items(i)
            If ([property].GetValue(item).Equals(key)) Then
                Return i
            End If
        Next
        Return -1 ' Not found

    End Function

    Protected Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return _isSorted
        End Get
    End Property

    Protected Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return _sortDirection
        End Get
    End Property

    Protected Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return _sortProperty
        End Get
    End Property

    Protected Overrides Sub ApplySortCore(ByVal [property] As PropertyDescriptor, ByVal direction As ListSortDirection)
        _sortDirection = direction
        _sortProperty = [property]
        Dim Comparer As SortComparer(Of T) = New SortComparer(Of T)([property], direction)
        ApplySortInternal(Comparer)
    End Sub

    Private Sub ApplySortInternal(ByVal comparer As SortComparer(Of T))

        If (_originalCollection.Count = 0) Then
            _originalCollection.AddRange(Me)
        End If
        Dim listRef As List(Of T) = CType(Me.Items, List(Of T))
        If (listRef Is Nothing) Then
            Return
        End If
        listRef.Sort(comparer)
        _isSorted = True
        OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
    End Sub

    Protected Overrides Sub RemoveSortCore()

        If (Not _isSorted) Then
            Return
        End If

        Clear()
        For Each item As T In _originalCollection
            Add(item)
        Next
        _originalCollection.Clear()
        _sortProperty = Nothing
        _sortDescriptions = Nothing
        _isSorted = False
    End Sub

    Public Sub ApplySort(ByVal sorts As ListSortDescriptionCollection) Implements IBindingListView.ApplySort
        _sortProperty = Nothing
        _sortDescriptions = sorts
        Dim Comparer As SortComparer(Of T) = New SortComparer(Of T)(sorts)
        ApplySortInternal(Comparer)
    End Sub

    Public Property Filter() As String Implements IBindingListView.Filter

        Get
            Return _filterString
        End Get

        Set(ByVal value As String)
            _filterPredicate = Nothing
            _filterString = value
            _isFiltered = True
            UpdateFilter()
        End Set

    End Property

    Public Property FilterPredicate() As Predicate(Of T)

        Get
            Return _filterPredicate
        End Get

        Set(ByVal value As Predicate(Of T))
            _filterString = Nothing
            _filterPredicate = value
            _isFiltered = True
            UpdateFilter()
        End Set

    End Property

    Public Sub RemoveFilter() Implements IBindingListView.RemoveFilter

        If (Not _isFiltered) Then
            Return
        End If
        _filterString = Nothing
        _filterPredicate = Nothing
        _isFiltered = False

        If (_originalCollection.Count < 1000) Then

            Clear()
            For Each item As T In _originalCollection
                Add(item)
            Next
            _originalCollection.Clear()

        Else

            Me.ClearItems()
            For Each item As T In _originalCollection

                Me.Items.Add(item)
            Next
            _originalCollection.Clear()
            If (Not _isSorted) Then
                Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, 0))
            End If
        End If

        If (_isSorted) Then
            Me.ApplySortCore(_sortProperty, _sortDirection)
        End If
    End Sub

    Public ReadOnly Property SortDescriptions() As ListSortDescriptionCollection Implements IBindingListView.SortDescriptions
        Get
            Return _sortDescriptions
        End Get
    End Property

    Public ReadOnly Property SupportsAdvancedSorting() As Boolean Implements IBindingListView.SupportsAdvancedSorting
        Get
            Return True
        End Get
    End Property

    Public ReadOnly Property SupportsFiltering() As Boolean Implements IBindingListView.SupportsFiltering
        Get
            Return True
        End Get
    End Property

    Protected Overridable Sub UpdateFilter()

        If (_originalCollection.Count = 0) Then
            _originalCollection.AddRange(Me)
        End If

        If (Not _filterString Is Nothing) Then
            Me.UpdateFilterFromString()
        ElseIf (Not _filterPredicate Is Nothing) Then
            Me.UpdateFilterFromPredicate()
        Else
            Me.RemoveFilter()
        End If

    End Sub

    Private Sub UpdateFilterFromString()

        Dim equalsPos As Integer = _filterString.IndexOf("=")
        ' Get property name
        Dim propName As String = _filterString.Substring(0, equalsPos).Trim()
        ' Get filter criteria
        Dim criteria As String = _filterString.Substring(equalsPos + 1, _filterString.Length - equalsPos - 1).Trim()
        ' Strip leading and trailing quotes
        If (criteria.Contains("""") Or criteria.Contains("'")) Then
            criteria = criteria.Substring(1, criteria.Length - 2)
        End If
        ' Get a property descriptor for the filter property
        Dim x As T 'GetProperties won't take type T; give it a variable of type T
        Dim propDesc As PropertyDescriptor = TypeDescriptor.GetProperties(x)(propName)
        Dim currentCollection As List(Of T) = New List(Of T)(Me)
        Clear()
        For Each item As T In currentCollection

            Dim value As Object = propDesc.GetValue(item)
            If (value.ToString() = criteria) Then
                Add(item)
            End If
        Next
    End Sub

    Private Sub UpdateFilterFromPredicate()

        Me.Items.Clear()
        For Each item As T In _originalCollection
            If (Me.FilterPredicate(item)) Then
                Me.Items.Add(item)
            End If
        Next

        If (_isSorted) Then
            Me.ApplySortCore(_sortProperty, _sortDirection)
        Else
            Me.OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, 0))
        End If
    End Sub

    Public Shadows ReadOnly Property AllowNew() As Boolean 'use Shadows because property is expected to be AllowNew and base is not virtual 
        Get
            Return CheckReadOnly()
        End Get
    End Property


    Public Shadows ReadOnly Property AllowRemove() As Boolean 'use Shadows because property is expected to be AllowRemove and base is not virtual 
        Get
            Return CheckReadOnly()
        End Get
    End Property

    Private Function CheckReadOnly() As Boolean

        If (_isSorted Or _isFiltered) Then
            Return False
        Else
            Return True
        End If

    End Function

    Protected Overrides Sub InsertItem(ByVal index As Integer, ByVal item As T)

        For Each propDesc As PropertyDescriptor In TypeDescriptor.GetProperties(item)
            If (propDesc.SupportsChangeEvents) Then
                propDesc.AddValueChanged(item, AddressOf OnItemChanged)
            End If
        Next
        MyBase.InsertItem(index, item)

    End Sub

    Protected Overrides Sub RemoveItem(ByVal index As Integer)

        Dim item As T = Items(index)
        Dim propDescs As PropertyDescriptorCollection = TypeDescriptor.GetProperties(item)

        For Each propDesc As PropertyDescriptor In propDescs
            If (propDesc.SupportsChangeEvents) Then
                propDesc.RemoveValueChanged(item, AddressOf OnItemChanged)
            End If
        Next
        MyBase.RemoveItem(index)

    End Sub

    Protected Sub OnItemChanged(ByVal sender As Object, ByVal args As EventArgs)
        Dim index As Integer = Items.IndexOf(CType(sender, T))
        OnListChanged(New ListChangedEventArgs(ListChangedType.ItemChanged, index))
    End Sub

    Public ReadOnly Property RaisesItemChangedEvents() As Boolean
        Get
            Return True
        End Get
    End Property

End Class

'End NameSpace
