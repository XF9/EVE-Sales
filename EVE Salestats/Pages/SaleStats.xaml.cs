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

using Windows.UI;

using EVE_SaleTools.Entities;
using EVE_SaleTools.Loader;

using SQLite;

using WinRTXamlToolkit.Controls.DataVisualization.Charting;

namespace EVE_SaleTools.Pages
{
    /// <summary>
    /// This page will display all transactions
    /// </summary>
    public sealed partial class SaleStats : Page
    {
        Character activeCharacter;

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
            activeCharacter = e.Parameter as Character;

            // load all transaction items
            var sqlite = new SQLiteAsyncConnection(activeCharacter.CharID + ".sqlite");

            List<Transaction> items = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' GROUP BY TypeID ORDER BY TypeName");
            this.Itemlist.ItemsSource = items;
            this.Categories.AddItems(items);
            this.Categories.ItemSelect += this.LoadData;

            SolidColorBrush line_sales = new SolidColorBrush(Color.FromArgb(255, 116, 195, 101));
            Style point_sales = new Style();
            point_sales.TargetType = typeof(DataPoint);
            point_sales.Setters.Add(new Setter(BackgroundProperty, line_sales));
            (LineChart.Series[0] as LineSeries).Background = line_sales;
            (LineChart.Series[0] as LineSeries).DataPointStyle = point_sales;

            SolidColorBrush line_salesAvg = new SolidColorBrush(Color.FromArgb(255, 34, 139, 34));
            Style point_salesAvg = new Style();
            point_salesAvg.TargetType = typeof(DataPoint);
            point_salesAvg.Setters.Add(new Setter(BackgroundProperty, line_salesAvg));
            point_salesAvg.Setters.Add(new Setter(WidthProperty, 0));
            point_salesAvg.Setters.Add(new Setter(HeightProperty, 0));
            (LineChart.Series[1] as LineSeries).Background = line_salesAvg;
            (LineChart.Series[1] as LineSeries).DataPointStyle = point_salesAvg;

            SolidColorBrush line_buys = new SolidColorBrush(Color.FromArgb(255, 196, 2, 51));
            Style point_buys = new Style();
            point_buys.TargetType = typeof(DataPoint);
            point_buys.Setters.Add(new Setter(BackgroundProperty, line_buys));
            (LineChart.Series[2] as LineSeries).Background = line_buys;
            (LineChart.Series[2] as LineSeries).DataPointStyle = point_buys;

            SolidColorBrush line_buysAvg = new SolidColorBrush(Color.FromArgb(255, 237, 28, 36));
            Style point_buysAvg = new Style();
            point_buysAvg.TargetType = typeof(DataPoint);
            point_buysAvg.Setters.Add(new Setter(BackgroundProperty, line_buysAvg));
            point_buysAvg.Setters.Add(new Setter(WidthProperty, 0));
            point_buysAvg.Setters.Add(new Setter(HeightProperty, 0));
            (LineChart.Series[3] as LineSeries).Background = line_buysAvg;
            (LineChart.Series[3] as LineSeries).DataPointStyle = point_buysAvg;
        }

        private async void LoadData(object sender, EVE_SaleTools.Templates.ItemSelectedEventArgs args)
        {
            var sqlite = new SQLiteAsyncConnection(activeCharacter.CharID + ".sqlite");
            List<Transaction> mexallonSalesPerDay = await sqlite.QueryAsync<Transaction>("SELECT Time, AVG(PricePerUnit) AS PricePerUnit FROM 'Transaction' WHERE TypeName = '" + args.transaction.TypeName + "' AND BuyOrder = 0 GROUP BY strftime('%d-%m-%Y',Time) ORDER BY Time ASC");

            if (mexallonSalesPerDay.Count > 0)
            {
                LineSeries sales = new LineSeries();
                (LineChart.Series[0] as LineSeries).ItemsSource = mexallonSalesPerDay;

                List<Transaction> mexallonSalesAveragePrice = await sqlite.QueryAsync<Transaction>("SELECT Time, AVG(PricePerUnit) AS PricePerUnit FROM 'Transaction' WHERE TypeName = '" + args.transaction.TypeName + "' AND BuyOrder = 0 GROUP BY TypeName");
                Transaction begin = new Transaction();
                begin.Time = mexallonSalesPerDay.Last().Time;
                begin.PricePerUnit = mexallonSalesAveragePrice.First().PricePerUnit;
                Transaction end = new Transaction();
                end.Time = mexallonSalesPerDay.First().Time;
                end.PricePerUnit = mexallonSalesAveragePrice.First().PricePerUnit;
                (LineChart.Series[1] as LineSeries).ItemsSource = new List<Transaction>() { begin, end };

            }


            List<Transaction> mexallonBuysPerDay = await sqlite.QueryAsync<Transaction>("SELECT Time, AVG(PricePerUnit) AS PricePerUnit FROM 'Transaction' WHERE TypeName = '" + args.transaction.TypeName + "' AND BuyOrder = 1 GROUP BY strftime('%d-%m-%Y',Time) ORDER BY Time ASC");
            if (mexallonBuysPerDay.Count > 0)
            {
                (LineChart.Series[2] as LineSeries).ItemsSource = mexallonBuysPerDay;


                List<Transaction> mexallonBuysAveragePrice = await sqlite.QueryAsync<Transaction>("SELECT Time, AVG(PricePerUnit) AS PricePerUnit FROM 'Transaction' WHERE TypeName = '" + args.transaction.TypeName + "' AND BuyOrder = 1 GROUP BY TypeName");
                Transaction begin = new Transaction();
                begin.Time = mexallonBuysPerDay.Last().Time;
                begin.PricePerUnit = mexallonBuysPerDay.First().PricePerUnit;
                Transaction end = new Transaction();
                end.Time = mexallonBuysPerDay.First().Time;
                end.PricePerUnit = mexallonBuysPerDay.First().PricePerUnit;
                (LineChart.Series[3] as LineSeries).ItemsSource = new List<Transaction>() { begin, end };

            }


            TransactionInformation meta = await TransactionInformation.Load(args.transaction.TypeID);
            this.ItemTitle.Text = args.transaction.TypeName;
            this.ItemDescription.Text = System.Text.RegularExpressions.Regex.Replace(meta.Description, "<.*?>", String.Empty);


            List<Transaction> mexallonAll = await sqlite.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE TypeName = '" + args.transaction.TypeName + "' ORDER BY Time");
            this.ListViewTransactions.ItemsSource = mexallonAll;
        }
    }
}
