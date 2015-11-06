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
    public class ItemSelectedEventArgs : EventArgs
    {
        public Transaction transaction;
    }

    public sealed partial class TransactionListCategoryList : UserControl
    {
        private Dictionary<String, TransactionListItemList> dataSet = new Dictionary<string,TransactionListItemList>();

        public EventHandler<ItemSelectedEventArgs> ItemSelect;

        public TransactionListCategoryList()
        {
            this.InitializeComponent();
        }

        private void MainList_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (TransactionListItemList entry in this.MainList.Items)
                entry.HideList();

            ((TransactionListItemList)e.ClickedItem).ShowList();
        }

        public void AddItems(List<Transaction> itemlist)
        {
            foreach (Transaction transaction in itemlist)
            {
                if (!this.dataSet.ContainsKey(transaction.MarketGroupName))
                {
                    TransactionListItemList newEntry = new TransactionListItemList();
                    newEntry.SetHeader(transaction.MarketGroupName);
                    newEntry.ItemSelect += PassClickEvent;

                    this.dataSet.Add(transaction.MarketGroupName, newEntry);
                    this.MainList.Items.Add(newEntry);
                }
                this.dataSet[transaction.MarketGroupName].AddEntry(transaction);
            }
        }

        private void PassClickEvent(object sender, ItemSelectedEventArgs args)
        {
            EventHandler<ItemSelectedEventArgs> handler = this.ItemSelect;
            if (handler != null)
                handler(this, args);
        }
    }
}
