using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{

    public sealed partial class SearchDialog : ContentDialog
    {
        double latitude1;
        double longitude1;

        DialogResult parent;

        private bool advancedSearch = false;
        public SearchDialog(ref DialogResult param)
        {
            this.InitializeComponent();

            parent = param;

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string cityName = CityName.Text;
            ComboBoxItem state = (ComboBoxItem)StateName.SelectedItem;
            string stateTag = null;

            if (state != null)
            {
                parent.fullAddress = true;
                stateTag = state.Tag.ToString();
            }
            else
            {
                parent.fullAddress = false;
            }

            if(cityName == "")
            {
                parent.fullAddress = false;
            }

            //string houseNumber;
            //string streetName;
            //string zipCode;

            string address = cityName + ", " + stateTag;

            parent.address = address;

        }

        public void TestMethod()
        {
            parent.Latitude = 20;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (advancedSearch == false)
            {
                //Item1.Visibility = Visibility.Visible;
                //Item2.Visibility = Visibility.Visible;
                //OrLabel.Visibility = Visibility.Collapsed;
                //DownArrow.Visibility = Visibility.Collapsed;
                //UpArrow.Visibility = Visibility.Visible;
                advancedSearch = true;
            }
            else
            {
                //Item1.Visibility = Visibility.Collapsed;
                //Item2.Visibility = Visibility.Collapsed;
                //OrLabel.Visibility = Visibility.Visible;
                //DownArrow.Visibility = Visibility.Visible;
                //UpArrow.Visibility = Visibility.Collapsed;

                advancedSearch = false;
            }

        }

        private void DownArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            //AdvnacedButton.Foreground = new SolidColorBrush(Colors.LightGray);
            //DownArrow.Foreground = new SolidColorBrush(Colors.LightGray);
            //UpArrow.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void DownArrow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            //AdvnacedButton.Foreground = new SolidColorBrush(Colors.Black);
            //DownArrow.Foreground = new SolidColorBrush(Colors.Black);
            //UpArrow.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            parent.Latitude = latitude1;
            parent.Longitude = longitude1;
        }
    }
}