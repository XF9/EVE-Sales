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

using EVE_Salestats.Char;

namespace EVE_Salestats.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CharacterSelection : Page
    {
        public CharacterSelection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Character[] characters = e.Parameter as Character[];
            Grid charGrid = new Grid();

            int index = 0;
            foreach (Character character in characters)
            {
                // create a field for every character
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = Windows.UI.Xaml.GridLength.Auto;
                charGrid.RowDefinitions.Add(rowDefinition);

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
                Grid.SetRowSpan(image, 2);
                Grid.SetRow(image, 0);

                // name
                TextBlock name = new TextBlock();
                name.Text = character.Name;
                name.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                name.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Bottom;
                name.Margin = new Thickness(10,0,0,0);
                name.FontWeight = Windows.UI.Text.FontWeights.Bold;
                name.FontSize = 20;
                Grid.SetColumn(name, 1);
                Grid.SetRow(name, 0);
                
                // corp
                TextBlock charInfo = new TextBlock();
                charInfo.Text = "\n" + character.Corp + "\n" + character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK";
                charInfo.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                charInfo.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                charInfo.Margin = new Thickness(10, 0, 0, 0);
                charInfo.FontWeight = Windows.UI.Text.FontWeights.Normal;
                charInfo.FontSize = 15;
                charInfo.TextWrapping = TextWrapping.WrapWholeWords;
                charInfo.TextAlignment = TextAlignment.Left;
                Grid.SetColumn(charInfo, 1);
                Grid.SetRow(charInfo, 1);

                ColumnDefinition col_image = new ColumnDefinition();
                ColumnDefinition col_data = new ColumnDefinition();

                RowDefinition row_name = new RowDefinition();
                RowDefinition row_info = new RowDefinition();
                RowDefinition row_space = new RowDefinition();

                col_image.Width = new GridLength(128, GridUnitType.Pixel);
                col_data.Width = new GridLength(1, GridUnitType.Star);

                row_name.Height = GridLength.Auto;
                row_info.Height = GridLength.Auto;
                row_space.Height = new GridLength(1, GridUnitType.Star);

                Grid characterData = new Grid();
                characterData.Width = 350;
                characterData.MinHeight = 128;
                characterData.ColumnDefinitions.Add(col_image);
                characterData.ColumnDefinitions.Add(col_data);
                characterData.RowDefinitions.Add(row_name);
                characterData.RowDefinitions.Add(row_info);
                characterData.RowDefinitions.Add(row_space);


                charGrid.Children.Add(characterBox);
                characterBox.Content = characterData;
                characterData.Children.Add(image);
                characterData.Children.Add(name);
                characterData.Children.Add(charInfo);
                //characterData.Children.Add(ballance);
            }

            this.MainGrid.Children.Add(charGrid);
            Grid.SetRow(charGrid, 2);
            Grid.SetColumn(charGrid, 1);
        }

        private void SelectCharacter(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EVE_Salestats.Pages.SaleStats));
        }
    }
}
