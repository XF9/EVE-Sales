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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EVE_Salestats.Templates
{
    public sealed partial class CharacterDisplay : UserControl
    {
        public CharacterDisplay(String name, String description, BitmapImage CharacterImage)
        {
            this.InitializeComponent();
            this.CharacterName.Text = name;
            this.CharacterDescription.Text = description;
            this.CharacterImage.Source = CharacterImage;
        }
    }
}
