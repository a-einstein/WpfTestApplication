﻿using System.Data;
using System.Windows;

namespace WpfTestApplication.BaseClasses
{
    public abstract class ItemsViewModel : ViewModel
    {
        // TODO This should become parameterized (like ItemViewModel), currently it assumes retrieval of entire table.
        public override void Refresh()
        {
            Items = GetData();
        }

        protected abstract DataView GetData();

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(DataView), typeof(ItemsViewModel));

        // Note this uses the DataView's standard filtering functionality.
        // Note this signals its own changes by IBindingListView, IBindingList.
        // Note a CollectionViewSource.View apparently is not able to filter.
        // This could also be implemented using a ObservableCollection and/or IQueryable.
        // TODO Change the type to some interface? Check references (like Items.Count and filtering).
        public DataView Items
        {
            get { return (DataView)GetValue(ItemsProperty); }
            set 
            { 
                SetValue(ItemsProperty, value);
                RaisePropertyChanged("ItemsCount");
            }
        }

        // Convenience property to signal changes.
        // Note that just binding on Items.Count does not work.
        public int ItemsCount { get { return Items != null ? Items.Count : 0; } }

        public virtual void OnItemChanged() {}
    }
}
