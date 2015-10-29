﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

using Windows.System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

using EVE_Salestats.Char;

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


namespace EVE_Salestats
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings;

        private String apiKey, vCode = "";

        public MainPage()
        {
            this.InitializeComponent();
            this.localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // check if data is stored and if so put it in the form
            if (localSettings.Values.ContainsKey("apikey") && localSettings.Values.ContainsKey("vCode"))
            {
                this.apiKey = localSettings.Values["apikey"].ToString();
                this.ApiKey.Text = localSettings.Values["apikey"].ToString();

                this.vCode = localSettings.Values["vCode"].ToString();
                this.VerificationCode.Text = localSettings.Values["vCode"].ToString();

                this.StoreData.IsChecked = true;
            }
        }

        /// <summary>
        /// Clickhandler fpr login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Errormsg.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.LoginButton.Content = "Fetching ..";

            apiKey = this.ApiKey.Text;
            vCode = this.VerificationCode.Text;

            if (this.StoreData.IsChecked.GetValueOrDefault(false))
            {
                localSettings.Values["apikey"] = this.apiKey;
                localSettings.Values["vCode"] = this.vCode;
            }
            else if (localSettings.Values.ContainsKey("apikey") && localSettings.Values.ContainsKey("vCode"))
            {
                localSettings.Values.Remove("apikey");
                localSettings.Values.Remove("vCode");
            }

            try{
                this.Frame.Navigate(typeof(CharacterSelection), await CharacterLoader.LoadCharacters(apiKey, vCode));
            }
            catch (Exception)
            {
                this.Errormsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.LoginButton.Content = "Login";
            }
        }
    }
}
