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

namespace EVE_Salestats
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
            Brush color_text = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 255, 255));
            Brush color_background = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 0, 32, 54));

            Character[] characters = e.Parameter as Character[];

            Grid charGrid = new Grid();
            ColumnDefinition spacerTop = new ColumnDefinition();
            ColumnDefinition spacerBottom = new ColumnDefinition();
            spacerTop.Width = new GridLength(1, GridUnitType.Star);
            spacerBottom.Width = new GridLength(1, GridUnitType.Star);
            charGrid.ColumnDefinitions.Add(spacerTop);

            int columnIndex = 1;
            foreach(Character character in characters){
                
                // create a field for every character
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(200);
                charGrid.ColumnDefinitions.Add(columnDefinition);
                
                // border arround
                Border characterBox = new Border();
                characterBox.BorderBrush = color_text;
                characterBox.BorderThickness = new Thickness(0.5);
                characterBox.Background = color_background;
                characterBox.Width = 160;
                characterBox.Height = 260;
                characterBox.Margin = new Thickness(10);
                characterBox.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                characterBox.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                Grid.SetColumn(characterBox, columnIndex);
                Grid.SetRow(characterBox, 0);

                // image
                Image image = new Image();
                image.Source = character.Image;
                image.Width = 128;
                image.Height = 128;
                image.Margin = new Thickness(10);
                Grid.SetColumn(image, 0);
                Grid.SetRow(image, 0);
                
                // name
                TextBlock name = new TextBlock();
                name.Text = character.Name;
                name.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                name.Margin = new Thickness(0, 0, 0, 0);
                name.FontWeight = Windows.UI.Text.FontWeights.Bold;
                name.FontSize = 15;
                Grid.SetColumn(name, 0);
                Grid.SetRow(name, 1);

                // corp
                TextBlock corp = new TextBlock();
                corp.Text = character.Corp;
                corp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                corp.Margin = new Thickness(0, 0, 0, 0);
                corp.FontWeight = Windows.UI.Text.FontWeights.Normal;
                corp.FontSize = 15;
                corp.TextWrapping = TextWrapping.WrapWholeWords;
                corp.Width = 140;
                corp.TextAlignment = TextAlignment.Center;
                Grid.SetColumn(corp, 0);
                Grid.SetRow(corp, 2);

                // ballance
                TextBlock ballance = new TextBlock();
                ballance.Text = character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK";
                ballance.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                ballance.Margin = new Thickness(0, 0, 0, 0);
                ballance.FontWeight = Windows.UI.Text.FontWeights.Normal;
                ballance.FontSize = 15;
                ballance.TextWrapping = TextWrapping.WrapWholeWords;
                Grid.SetColumn(ballance, 0);
                Grid.SetRow(ballance, 3);

                RowDefinition row_image = new RowDefinition();
                RowDefinition row_name = new RowDefinition();
                RowDefinition row_corp = new RowDefinition();
                RowDefinition row_balance = new RowDefinition();
                RowDefinition row_space = new RowDefinition();
                row_image.Height = new GridLength(160, GridUnitType.Pixel);
                row_name.Height = GridLength.Auto;
                row_corp.Height = GridLength.Auto;
                row_balance.Height = GridLength.Auto;
                row_space.Height = new GridLength(1, GridUnitType.Star);
                Grid characterData = new Grid();
                characterData.RowDefinitions.Add(row_image);
                characterData.RowDefinitions.Add(row_name);
                characterData.RowDefinitions.Add(row_corp);
                characterData.RowDefinitions.Add(row_balance);
                characterData.RowDefinitions.Add(row_space);

                charGrid.Children.Add(characterBox);
                characterBox.Child = characterData;
                characterData.Children.Add(image);
                characterData.Children.Add(name);
                characterData.Children.Add(corp);
                characterData.Children.Add(ballance);
                columnIndex++;
            }

            charGrid.ColumnDefinitions.Add(spacerBottom);
            this.MainGrid.Children.Add(charGrid);
            Grid.SetRow(charGrid, 2);
            Grid.SetColumn(charGrid, 1);
        }
    }
}
