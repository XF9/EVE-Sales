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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

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
            System.Diagnostics.Debug.WriteLine(characters[0].Name);

            Grid charGrid = new Grid();

            ColumnDefinition spacer_top = new ColumnDefinition();
            ColumnDefinition spacer_bottom = new ColumnDefinition();
            spacer_top.Width = new GridLength(1, GridUnitType.Star);
            spacer_bottom.Width = new GridLength(1, GridUnitType.Star);

            charGrid.ColumnDefinitions.Add(spacer_top);

            int columnIndex = 1;
            foreach(Character character in characters){
                
                // create a field for every character
                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(200);
                charGrid.ColumnDefinitions.Add(columnDefinition);
                
                // border arround
                Border border = new Border();
                border.BorderBrush = color_text;
                border.BorderThickness = new Thickness(0.5);
                border.Background = color_background;
                border.Width = 160;
                border.Height = 260;
                border.Margin = new Thickness(20);
                border.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top;
                border.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left;
                Grid.SetColumn(border, columnIndex);
                Grid.SetRow(border, 0);

                // name
                TextBlock name = new TextBlock();
                name.Text = character.Name;
                name.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                name.Margin = new Thickness(0, 160, 0, 0);
                name.FontWeight = Windows.UI.Text.FontWeights.Bold;
                name.FontSize = 15;
                Grid.SetColumn(name, columnIndex);
                Grid.SetRow(name, 0);

                // name
                TextBlock corp = new TextBlock();
                corp.Text = character.Corp;
                corp.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                corp.Margin = new Thickness(0, 190, 0, 0);
                corp.FontWeight = Windows.UI.Text.FontWeights.Normal;
                corp.FontSize = 15;
                corp.TextWrapping = TextWrapping.WrapWholeWords;
                Grid.SetColumn(corp, columnIndex);
                Grid.SetRow(corp, 0);

                // name
                TextBlock ballance = new TextBlock();
                ballance.Text = character.Ballance + " ISK";
                ballance.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
                ballance.Margin = new Thickness(0, 220, 0, 0);
                ballance.FontWeight = Windows.UI.Text.FontWeights.Normal;
                ballance.FontSize = 15;
                ballance.TextWrapping = TextWrapping.WrapWholeWords;
                Grid.SetColumn(ballance, columnIndex);
                Grid.SetRow(ballance, 0);

                charGrid.Children.Add(border);
                charGrid.Children.Add(name);
                charGrid.Children.Add(corp);
                charGrid.Children.Add(ballance);
                columnIndex++;

                System.Diagnostics.Debug.WriteLine(character.Ballance);
            }

            charGrid.ColumnDefinitions.Add(spacer_bottom);

            this.MainGrid.Children.Add(charGrid);
            Grid.SetRow(charGrid, 2);
        }
    }
}
