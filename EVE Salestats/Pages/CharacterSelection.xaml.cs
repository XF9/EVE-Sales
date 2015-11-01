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
using EVE_Salestats.Templates;

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

            int index = 0;
            foreach (Character character in characters)
            {
                // create a field for every character
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = Windows.UI.Xaml.GridLength.Auto;
                this.CharGrid.RowDefinitions.Add(rowDefinition);

                CharacterDisplay charInfo = new CharacterDisplay(character.Name, character.Corp + "\n" + character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK", character.Image);
                Grid.SetRow(charInfo, index++);
                Grid.SetColumn(charInfo, 0);

                this.CharGrid.Children.Add(charInfo);
            }
        }

        private void SelectCharacter(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EVE_Salestats.Pages.SaleStats));
        }
    }
}
