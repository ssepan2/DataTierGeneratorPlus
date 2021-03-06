using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Cs_BuiltIn {

    public class BindingListView<T> : BindingList<T>, IBindingListView, IRaiseItemChangedEvents
      {
            private Boolean _isSorted = false;
            private Boolean _isFiltered = false;
            private String _filterString = null;
            private Predicate<T> _filterPredicate = null;
            private ListSortDirection _sortDirection = ListSortDirection.Ascending;
            private PropertyDescriptor _sortProperty = null;
            private ListSortDescriptionCollection _sortDescriptions = new ListSortDescriptionCollection();
            private List<T> _originalCollection = new List<T>();
 
            public BindingListView() : base()
            {
            }
 
            public BindingListView(List<T> list) : base(list)
            {
            }
 
            protected override Boolean SupportsSearchingCore
            {
                  get { return true; }
            }
 
            protected override int FindCore(PropertyDescriptor property, object key)
            {
                  // Simple iteration:
                  for (int i = 0; i < Count; i++)
                  {
                        T item = this[i];
                        if (property.GetValue(item).Equals(key))
                        {
                              return i;
                        }
                  }
                  return -1; // Not found
            }
 
            protected override Boolean SupportsSortingCore
            {
                  get { return true; }
            }
 
            protected override Boolean IsSortedCore
            {
                  get { return _isSorted; }
            }
 
            protected override ListSortDirection SortDirectionCore
            {
                  get { return _sortDirection; }
            }
 
            protected override PropertyDescriptor SortPropertyCore
            {
                  get { return _sortProperty; }
            }
 
            protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection direction)
            {
                  _sortDirection = direction;
                  _sortProperty = property;
                  SortComparer<T> comparer = new SortComparer<T>(property, direction);
                  ApplySortInternal(comparer);
            }
 
            private void ApplySortInternal(SortComparer<T> comparer)
            {
                  if (_originalCollection.Count == 0)
                  {
                        _originalCollection.AddRange(this);
                  }
                  List<T> listRef = this.Items as List<T>;
                  if (listRef == null)
                        return;
                  listRef.Sort(comparer);
                  _isSorted = true;
                  OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
 
            protected override void RemoveSortCore()
            {
                if (!_isSorted)
                {
                    return;
                }
 
                  Clear();
 
                  foreach (T item in _originalCollection)
                  {
                        Add(item);
                  }
                  _originalCollection.Clear();
                  _sortProperty = null;
                  _sortDescriptions = null;
                  _isSorted = false;
            }
 
            public void ApplySort(ListSortDescriptionCollection sorts)
            {
                  _sortProperty = null;
                  _sortDescriptions = sorts;
                  SortComparer<T> comparer = new SortComparer<T>(sorts);
                  ApplySortInternal(comparer);
            }
 
            public String Filter
            {
                  get { return _filterString; }
                  set
                  {
                        _filterPredicate = null;
                        _filterString = value;
                        _isFiltered = true;
                        UpdateFilter();
                  }
            }
 
            public Predicate<T> FilterPredicate
            {
                  get { return _filterPredicate; }
                  set
                  {
                        _filterString = null;
                        _filterPredicate = value;
                        _isFiltered = true;
                        UpdateFilter();
                  }
            }
 
            public void RemoveFilter()
            {
                if (!_isFiltered)
                {
                    return;
                }

                  _filterString = null;
                  _filterPredicate = null;
                  _isFiltered = false;
 
                  if (_originalCollection.Count < 1000)
                  {
                        Clear();
                        foreach (T item in _originalCollection)
                        {
                              Add(item);
                        }
                        _originalCollection.Clear();
                  }
                  else
                  {
                        this.ClearItems();
                        foreach (T item in _originalCollection)
                        {
                              this.Items.Add(item);
                        }
                        _originalCollection.Clear();
                        if (!_isSorted)
                        {
                            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
                        }
                  }

                  if (_isSorted)
                  {
                      this.ApplySortCore(_sortProperty, _sortDirection);
                  }
            }
 
            public ListSortDescriptionCollection SortDescriptions
            {
                  get { return _sortDescriptions; }
            }
 
            public Boolean SupportsAdvancedSorting
            {
                  get { return true; }
            }
 
            public Boolean SupportsFiltering
            {
                  get { return true; }
            }
 
            protected virtual void UpdateFilter()
            {
                  if (_originalCollection.Count == 0)
                  {
                        _originalCollection.AddRange(this);
                  }
 
                  if (_filterString != null)
                  {
                        this.UpdateFilterFromString();
                  }
                  else if (_filterPredicate != null)
                  {
                        this.UpdateFilterFromPredicate();
                  }
                  else
                  {
                        this.RemoveFilter();
                  }
            }
 
            private void UpdateFilterFromString()
            {
                  int equalsPos = _filterString.IndexOf('=');
                  // Get property name
                  String propName = _filterString.Substring(0, equalsPos).Trim();
                  // Get filter criteria
                  String criteria = _filterString.Substring(equalsPos + 1, _filterString.Length - equalsPos - 1).Trim();
                  // Strip leading and trailing quotes
                  if (criteria.Contains("\"") || criteria.Contains("'"))
                  {
                        criteria = criteria.Substring(1, criteria.Length - 2);
                  }
                  // Get a property descriptor for the filter property
                  PropertyDescriptor propDesc = TypeDescriptor.GetProperties(typeof(T))[propName];
                  List<T> currentCollection = new List<T>(this);
                  Clear();
                  foreach (T item in currentCollection)
                  {
                        object value = propDesc.GetValue(item);
                        if (value.ToString() == criteria)
                        {
                              Add(item);
                        }
                  }
            }
 
            private void UpdateFilterFromPredicate()
            {
                  this.Items.Clear();
                  foreach (T item in _originalCollection)
                  {
                        if (this.FilterPredicate(item))
                        {
                              this.Items.Add(item);
                        }
                  }
 
                  if (_isSorted)
                        this.ApplySortCore(_sortProperty, _sortDirection);
                  else
                        this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
            }
 
            public new Boolean AllowNew //use new because property is expected to be AllowNew and base is not virtual 
            {
                  get { return CheckReadOnly(); }
            }
 
            public new Boolean AllowRemove //use new because property is expected to be AllowRemove and base is not virtual 
            {
                  get { return CheckReadOnly(); }
            }

            private Boolean CheckReadOnly()
            {
                  if (_isSorted || _isFiltered)
                  {
                        return false;
                  }
                  else
                  {
                        return true;
                  }
            }
 
            protected override void InsertItem(int index, T item)
            {
                  foreach (PropertyDescriptor propDesc in TypeDescriptor.GetProperties(item))
                  {
                        if (propDesc.SupportsChangeEvents)
                        {
                              propDesc.AddValueChanged(item, OnItemChanged);
                        }
                  }
                  base.InsertItem(index, item);
            }
 
            protected override void RemoveItem(int index)
            {
                  T item = Items[index];
                 PropertyDescriptorCollection propDescs = TypeDescriptor.GetProperties(item);

                  foreach (PropertyDescriptor propDesc in propDescs)
                  {
                        if (propDesc.SupportsChangeEvents)
                        {
                              propDesc.RemoveValueChanged(item, OnItemChanged);
                        }
                  }
                  base.RemoveItem(index);
            }
 
            protected void OnItemChanged(Object sender, EventArgs args)
            {
                  int index = Items.IndexOf((T)sender);
                  OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
            }
 
            public Boolean RaisesItemChangedEvents
            {
                  get { return true; }
            }
 
      }

}
