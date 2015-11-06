using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using EVE_SaleTools.Entities;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EVE_SaleTools.Templates
{
    public sealed partial class TransactionListItemList : UserControl
    {
        public TransactionListItemList()
        {
            this.InitializeComponent();
        }

        public void HideList()
        {
            this.ItemList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void ShowList()
        {
            this.ItemList.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        public void ToggleList(){
            this.ItemList.Visibility = this.ItemList.Visibility == Windows.UI.Xaml.Visibility.Collapsed ? Windows.UI.Xaml.Visibility.Visible : Windows.UI.Xaml.Visibility.Collapsed;
        }

        public void SetHeader(string header)
        {
            this.CategoryName.Text = header;
        }

        public void AddEntry(Transaction ta)
        {
            ListViewItem entry = new ListViewItem();
            entry.Content = ta.TypeName;
            this.ItemList.Items.Add(entry);
        }


    }
}
