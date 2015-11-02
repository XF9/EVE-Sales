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

using WinRTXamlToolkit.Controls.DataVisualization.Charting;

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
            List<Transaction> transactions = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' GROUP BY TypeID ORDER BY TypeName");
            this.Itemlist.ItemsSource = transactions;

            List<Transaction> mexallon = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE TypeName = 'Mexallon' ORDER BY TypeName");
            (LineChart.Series[0] as LineSeries).ItemsSource = mexallon;
        }
    }
}
