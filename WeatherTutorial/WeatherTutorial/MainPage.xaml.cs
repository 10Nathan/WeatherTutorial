using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SQLite.Net.Attributes;

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
            

            hamburgerColor = MapBackground.Background;
        }

        private Brush hamburgerColor;
        private bool MapClicked = false;
        private bool WeatherClicked = false;
        private bool TravelClicked = false;
        private bool SettingsClicked = false;
        private MapPage mapPage;

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if(radioButton != null)
            {
                switch(radioButton.Tag.ToString())
                {
                    case "Map":
                        Disable_DropDownItems();
                        radioMark.Visibility = Visibility.Visible;
                        MainFrame.Navigate(typeof(MapPage));

                        break;
                    case "Weather":
                        MainFrame.Name = "WeatherPage";
                        Disable_DropDownItems();
                        weatherMark.Visibility = Visibility.Visible;
                        MainFrame.Navigate(typeof(WeatherPage));
                        break;
                    case "Travel":
                        Disable_DropDownItems();
                        travelMark.Visibility = Visibility.Visible;
                        MainFrame.Navigate(typeof(TravelPage));
                        break;
                    case "Settings":
                        Disable_DropDownItems();
                        settingsMark.Visibility = Visibility.Visible;
                        MainFrame.Navigate(typeof(SettingsPage));
                        break;
                        
                }
            }
        }

        private void OpenHamburgerButton(object sender, RoutedEventArgs e)
        {
            this.MySplitView.IsPaneOpen = this.MySplitView.IsPaneOpen ? false : true;
        }

        private void RadioButton_PointerEnter(object sender, PointerRoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            switch (radioButton.Tag.ToString())
            {
                case "Map":
                    MapBackground.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case "Weather":
                    WeatherBackground.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case "Travel":
                    Travelbackground.Background = new SolidColorBrush(Colors.LightGray);
                    break;
                case "Settings":
                    SettingBackground.Background = new SolidColorBrush(Colors.LightGray);
                    break;

            }
        }

        private void RadioButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            switch (radioButton.Tag.ToString())
            {
                case "Map":
                    MapBackground.Background = hamburgerColor;
                    break;
                case "Weather":
                    WeatherBackground.Background = hamburgerColor;
                    break;
                case "Travel":
                    Travelbackground.Background = hamburgerColor;
                    break;
                case "Settings":
                    SettingBackground.Background = hamburgerColor;
                    break;

            }
        }

        public void Disable_DropDownItems()
        {
            radioMark.Visibility = Visibility.Collapsed;
            weatherMark.Visibility = Visibility.Collapsed;
            travelMark.Visibility = Visibility.Collapsed;
            settingsMark.Visibility = Visibility.Collapsed;

        }

    }
}
