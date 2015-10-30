using System;
using Windows.UI.Xaml.Media.Animation;

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
            this.CharList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
                //this.DisplayCharacterList(await CharacterLoader.LoadCharacters(apiKey, vCode));
            }
            catch (Exception)
            {
                this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Visible;
                this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                this.CharList.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
        }

        private void DisplayCharacterList(Character[] characters)
        {
            int index = 0;
            foreach (Character character in characters)
            {
                // create a field for every character
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = Windows.UI.Xaml.GridLength.Auto;
                this.CharList.RowDefinitions.Add(rowDefinition);

                // char button
                Button characterBox = new Button();
                //characterBox.MinHeight = 148;
                characterBox.Margin = new Thickness(0);
                characterBox.BorderThickness = new Thickness(0.5);
                characterBox.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
                characterBox.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                Grid.SetColumn(characterBox, 0);
                Grid.SetRow(characterBox, index++);

                // image
                Image image = new Image();
                image.Source = character.Image;
                image.Width = 128;
                image.Height = 128;
                image.Margin = new Thickness(0);
                image.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
                image.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
                Grid.SetColumn(image, 0);
                Grid.SetRowSpan(image, 3);
                Grid.SetRow(image, 0);

                // name
                TextBlock name = new TextBlock();
                name.Text = character.Name + "\n" + character.Corp + "\n" + character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK";
                name.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                name.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
                name.Margin = new Thickness(10,0,0,0);
                //name.FontWeight = Windows.UI.Text.FontWeights.Bold;
                name.FontSize = 15;
                Grid.SetColumn(name, 1);
                Grid.SetRow(name, 0);
                /*
                // corp
                TextBlock corp = new TextBlock();
                corp.Text = character.Corp;
                corp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                corp.Margin = new Thickness(0);
                corp.FontWeight = Windows.UI.Text.FontWeights.Normal;
                corp.FontSize = 15;
                corp.TextWrapping = TextWrapping.WrapWholeWords;
                corp.TextAlignment = TextAlignment.Left;
                Grid.SetColumn(corp, 1);
                Grid.SetRow(corp, 1);

                // ballance
                TextBlock ballance = new TextBlock();
                ballance.Text = character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK";
                ballance.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                ballance.Margin = new Thickness(0);
                ballance.FontWeight = Windows.UI.Text.FontWeights.Normal;
                ballance.FontSize = 15;
                ballance.TextWrapping = TextWrapping.WrapWholeWords;
                corp.TextAlignment = TextAlignment.Left;
                Grid.SetColumn(ballance, 1);
                Grid.SetRow(ballance, 2);
                */

                ColumnDefinition col_image = new ColumnDefinition();
                ColumnDefinition col_data = new ColumnDefinition();

                col_image.Width = new GridLength(128, GridUnitType.Pixel);
                col_data.Width = new GridLength(1, GridUnitType.Star);

                Grid characterData = new Grid();
                characterData.Width = 350;
                characterData.MinHeight = 128;
                characterData.ColumnDefinitions.Add(col_image);
                characterData.ColumnDefinitions.Add(col_data);


                this.CharList.Children.Add(characterBox);
                characterBox.Content = characterData;
                characterData.Children.Add(image);
                characterData.Children.Add(name);
                //characterData.Children.Add(corp);
                //characterData.Children.Add(ballance);
            }

            this.LoginForm.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.LoadingMessage.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.CharList.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }
    }
}
