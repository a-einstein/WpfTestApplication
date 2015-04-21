﻿using System;

namespace WpfTestApplication.ViewModels
{
    class MainViewModel : IMainViewModel
    {
        private Uri aboutPageSource = new Uri("/Views/AboutView.xaml", UriKind.Relative);

        public Uri AboutPageSource
        {
            get { return aboutPageSource; }
            set { aboutPageSource = value; }
        }

        private Uri productsPageSource = new Uri("/Views/ProductsView.xaml", UriKind.Relative);

        public Uri ProductsPageSource
        {
            get { return productsPageSource; }
            set { productsPageSource = value; }
        }
    }

    interface IMainViewModel
    {
        Uri AboutPageSource { get; set; }
        Uri ProductsPageSource { get; set; }
    }
}
