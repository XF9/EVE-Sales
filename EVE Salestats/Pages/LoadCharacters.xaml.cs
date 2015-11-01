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
    /// This page will load all characters and forward to character selection after loading
    /// </summary>
    public sealed partial class LoadCharacters : Page
    {
        public LoadCharacters()
        {
            this.InitializeComponent();
        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            AccountInfo accountInfo = e.Parameter as AccountInfo;

            Character[] characters = await CharacterLoader.LoadCharacters(accountInfo.ApiKey, accountInfo.VCode);
            this.Frame.Navigate(typeof(CharacterSelection), characters);
        }
    }
}
