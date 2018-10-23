using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class MainMenu : Page
    {
        public MainMenu()
        {
            this.InitializeComponent();
        }

        private void Nav_Menu_Loaded(object sender, RoutedEventArgs e)
        {
            // set the initial SelectedItem
            foreach (NavigationViewItemBase item in Nav_Menu.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home")
                {
                    Nav_Menu.SelectedItem = item;
                    break;
                }
            }
            Content_Header.Text = "Home";
            contentFrame.Navigate(typeof(Views.Home));
        }

        private void Nav_Menu_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            Debug.WriteLine(ItemContent.Tag);
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_Home":
                        Content_Header.Text = "Home";
                        contentFrame.Navigate(typeof(Views.Home));
                        break;

                    case "Nav_Music":
                        Content_Header.Text = "Music";
                        contentFrame.Navigate(typeof(Views.Music));
                        break;

                    case "Nav_Album":
                        Content_Header.Text = "Album";
                        contentFrame.Navigate(typeof(Views.Ablum));
                        break;

                    case "Nav_Account":
                        Content_Header.Text = "Account";
                        contentFrame.Navigate(typeof(Views.Account));
                        break;
                }
            }
        }

        
    }
}
