﻿using Common.DomainClasses;
using Demo.Common;
using Demo.Modules.Products.Model;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace Demo.Modules.Products.ViewModels
{
    [Export]
    public class ShoppingCartViewModel : ItemsViewModel<CartItem, object>
    {
        private ShoppingCartViewModel()
        {
            // HACK Typing is unclear here.
            // Besides, this currently is the only binding to a repository List.
            Items = CartItemsRepository.Instance.List as ObservableCollection<CartItem>;

            (CartItemsRepository.Instance.List as ObservableCollection<CartItem>).CollectionChanged += List_CollectionChanged;
        }

        private static volatile ShoppingCartViewModel instance;
        private static object syncRoot = new Object();

        [Export("WidgetViewModel", typeof(ViewModel))]
        // Note this class is a singleton, implemented along the way (but not entirely) of https://msdn.microsoft.com/en-us/library/ff650316.aspx
        // TODO This might no longer be necessary if properly shared on import.
        public static ShoppingCartViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ShoppingCartViewModel();
                    }
                }

                return instance;
            }
        }

        protected override void SetCommands()
        {
            base.SetCommands();

            DeleteCommand = new DelegateCommand<CartItem>(Delete);
        }

        public void CartProduct(IShoppingProduct productsOverviewObject)
        {
            CartItemsRepository.Instance.AddProduct(productsOverviewObject);
        }

        public ICommand DeleteCommand { get; set; }

        private void Delete(CartItem cartItem)
        {
            CartItemsRepository.Instance.DeleteProduct(cartItem);
        }

        private void List_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    (e.NewItems[0] as CartItem).PropertyChanged += CartItem_PropertyChanged;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    (e.OldItems[0] as CartItem).PropertyChanged -= CartItem_PropertyChanged;
                    break;
            }

            // TODO Put this as calculated set in UpdateAggregates too?
            RaisePropertyChanged("ItemsCount");

            UpdateAggregates();

        }

        private void CartItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName== "Quantity")
            {
                UpdateAggregates();
            }
        }

        private void UpdateAggregates()
        {
            ProductItemsCount = CartItemsRepository.Instance.ProductsCount();
            TotalValue = CartItemsRepository.Instance.CartValue();
        }

        public static readonly DependencyProperty ProductItemCountProperty =
            DependencyProperty.Register("ProductItemsCount", typeof(int), typeof(ShoppingCartViewModel), new PropertyMetadata(0));

        public int ProductItemsCount
        {
            get { return (int)GetValue(ProductItemCountProperty); }
            set { SetValue(ProductItemCountProperty, value); }
        }

        public static readonly DependencyProperty TotalValueProperty =
            DependencyProperty.Register("TotalValue", typeof(Decimal), typeof(ShoppingCartViewModel), new PropertyMetadata((Decimal)0));

        public Decimal TotalValue
        {
            get { return (Decimal)GetValue(TotalValueProperty); }
            set { SetValue(TotalValueProperty, value); }
        }
    }
}
