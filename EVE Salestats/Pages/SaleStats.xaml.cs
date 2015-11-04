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

using SQLite;

using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace EVE_SaleTools.Pages
{
    /// <summary>
    /// This page will display all transactions
    /// </summary>
    public sealed partial class SaleStats : Page
    {
        public SaleStats()
        {
            this.InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EVE_SaleTools.Pages.CharacterSelection));
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Character activeCharacter = e.Parameter as Character;

            // load all transaction items
            var sqlite = new SQLiteAsyncConnection(activeCharacter.CharID);
            List<Transaction> transactions = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' GROUP BY TypeID ORDER BY TypeName");
            this.Itemlist.ItemsSource = transactions;

            // tmp - query mexallon stats
            List<Transaction> mexallon = await sqlite.QueryAsync<Transaction>("SELECT Time, AVG(PricePerUnit) AS PricePerUnit FROM 'Transaction' WHERE TypeName = 'Mexallon' AND BuyOrder = 0 GROUP BY strftime('%d-%m-%Y',Time)");
            (LineChart.Series[0] as LineSeries).ItemsSource = mexallon;
        }
    }
}
