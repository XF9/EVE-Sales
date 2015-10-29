using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Xml.Linq;

using Windows.System;

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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

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
        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            this.Errormsg.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.LoginButton.Content = "Fetching ..";

            this.apiKey = this.ApiKey.Text;
            this.vCode = this.VerificationCode.Text;

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

            this.LoadCharacters();
        }

        /// <summary>
        /// Load character data
        /// </summary>
        async private void LoadCharacters()
        {
            String request = "https://api.eveonline.com/account/Characters.xml.aspx?keyID=" + this.apiKey + "&vCode=" + this.vCode;

            try
            {
                HttpResponseMessage response = await new HttpClient().GetAsync(request);
                string xml_data = await response.Content.ReadAsStringAsync();

                XDocument characterdata = XDocument.Parse(xml_data);

                var characters = from character in characterdata.Descendants("row")
                               select new
                               {
                                   Name = character.Attribute("name").Value,
                                   CharID = character.Attribute("characterID").Value
                               };

                Character[] charlist = new Character[characters.Count()];
                int index = 0;

                foreach (var character in characters)
                {
                    Character charcter = new Character(character.Name, character.CharID, "Nope", 1000.0f);
                    charlist[index++] = charcter;
                }

                this.Frame.Navigate(typeof(CharacterSelection), charlist);
            }
            catch (Exception e)
            {
                this.Errormsg.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.LoginButton.Content = "Login";
            }
        }
    }
}
