using System;

using EVE_Salestats.Char;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace EVE_Salestats.Pages
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
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
            this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Visible;

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
                Character[] characters = await CharacterLoader.LoadCharacters(apiKey, vCode);
                this.Frame.Navigate(typeof(CharacterSelection), characters);
            }
            catch (Exception)
            {
                this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }
    }
}
