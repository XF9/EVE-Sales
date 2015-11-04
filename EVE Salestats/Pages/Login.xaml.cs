using System;
using Windows.UI.Xaml.Media.Animation;

using EVE_SaleTools.Entities;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace EVE_SaleTools.Pages
{

    /// <summary>
    /// This page will gather the api key and verification code if needed
    /// </summary>
    public sealed partial class Login : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings;

        private String apiKey, vCode = "";

        public Login()
        {
            this.InitializeComponent();
            this.localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // check if data is stored and if so put it in the form
            // TODO autoforward
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
        /// Clickhandler for login button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;

            apiKey = this.ApiKey.Text;
            vCode = this.VerificationCode.Text;

            // store or delete local data if needed
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
                this.Frame.Navigate(typeof(EVE_SaleTools.Pages.LoadCharacters), new AccountInfo(apiKey, vCode));
            }
            catch (Exception)
            {
                // revert to default if something fails
                // TODO Errormessage
                this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
