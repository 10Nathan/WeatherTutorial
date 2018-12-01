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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if(radioButton != null)
            {
                switch(radioButton.Tag.ToString())
                {
                    case "Map":
                        MainFrame.Navigate(typeof(MapPage));
                        AerialIcon.Visibility = Visibility.Visible;
                        AerialText.Visibility = Visibility.Visible;
                        break;
                    case "Weather":
                        MainFrame.Navigate(typeof(WeatherPage));
                        AerialIcon.Visibility = Visibility.Collapsed;
                        AerialText.Visibility = Visibility.Collapsed;
                        break;
                    case "Travel":
                        MainFrame.Navigate(typeof(TravelPage));
                        break;
                    case "Settings":
                        MainFrame.Navigate(typeof(SettingsPage));
                        break;
                        
                }
            }
        }

        private void OpenHamburgerButton(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = this.MySplitView.IsPaneOpen ? false : true;
        }


    }
}
