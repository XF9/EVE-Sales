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

using EVE_Salestats.Entities;

using SQLite;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVE_Salestats.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SaleStats : Page
    {
        public SaleStats()
        {
            this.InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EVE_Salestats.Pages.CharacterSelection));
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Character activeCharacter = e.Parameter as Character;

            var sqlite = new SQLiteAsyncConnection(activeCharacter.CharID);
            List<Transaction> transactions = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' GROUP BY TypeID");
            /*
            foreach (Transaction transaction in transactions)
            {
                ListViewItem tmp = new ListViewItem();
                tmp.Content = transaction;
                this.Itemlist.Items.Add(tmp);
            }
             * */
            this.Itemlist.ItemsSource = transactions;
        }
    }
}
