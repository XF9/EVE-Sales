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
using EVE_SaleTools.Templates;

namespace EVE_SaleTools.Pages
{
    /// <summary>
    /// This page let's you select the character whoms transaction data should be loaded
    /// </summary>
    public sealed partial class CharacterSelection : Page
    {
        /// <summary>
        /// Ctor.
        /// </summary>
        public CharacterSelection()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            int index = 0;
            foreach (Character character in Settings.characterList)
            {
                // create a field for every character
                RowDefinition rowDefinition = new RowDefinition();
                rowDefinition.Height = Windows.UI.Xaml.GridLength.Auto;
                this.CharGrid.RowDefinitions.Add(rowDefinition);

                CharacterDisplay charInfo = new CharacterDisplay(character);
                Grid.SetRow(charInfo, index++);
                Grid.SetColumn(charInfo, 0);

                this.CharGrid.Children.Add(charInfo);

                charInfo.OnSelect += SelectCharacter;
            }
        }

        private void SelectCharacter(object sender, Character character)
        {
            this.Frame.Navigate(typeof(EVE_SaleTools.Pages.LoadTransactions), character);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EVE_SaleTools.Pages.Login));
        }
    }
}
