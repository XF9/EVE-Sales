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
using EVE_SaleTools.Loader;

using SQLite;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVE_SaleTools.Pages
{
    /// <summary>
    /// This page will load all transactions
    /// </summary>
    public sealed partial class LoadTransactions : Page
    {
        public LoadTransactions()
        {
            this.InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Load
            Character character = e.Parameter as Character;
            await TransactionLoader.Parse(Settings.accountInformation.ApiKey, Settings.accountInformation.VCode, character.CharID);

            // Forward
            this.Frame.Navigate(typeof(SaleStats), character);
        }
    }
}
