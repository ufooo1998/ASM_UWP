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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ASM.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();
        }
        private string Current_Tag;
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (Current_Tag == radio.Tag.ToString())
            {
                return;
            }
            switch (radio.Tag.ToString())
            {
                case "Personal":
                    Current_Tag = "Personal";
                    this.MainContent.Navigate(typeof(Views.Form));
                    break;
                case "Media_View":
                    Current_Tag = "Media_View";
                    this.MainContent.Navigate(typeof(Views.MediaViews));
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.MainMenu.IsPaneOpen = !this.MainMenu.IsPaneOpen;
        }
    }
}
