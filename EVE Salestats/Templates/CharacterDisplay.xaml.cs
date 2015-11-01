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

using Windows.UI.Xaml.Media.Imaging;
using EVE_Salestats.Entities;

namespace EVE_Salestats.Templates
{
    public sealed partial class CharacterDisplay : UserControl
    {
        private Character character;

        public event EventHandler<Character> OnSelect;

        protected void EmitEvent(Character e)
        {
            EventHandler<Character> handler = OnSelect;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public CharacterDisplay(Character character)
        {
            this.InitializeComponent();
            this.character = character;
            this.CharacterName.Text = character.Name;
            this.CharacterDescription.Text = character.Corp + "\n" + character.Ballance.ToString("n", EVE_Salestats.Settings.numberFormat) + " ISK";
            this.CharacterImage.Source = character.Image;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EmitEvent(this.character);
        }
    }
}
