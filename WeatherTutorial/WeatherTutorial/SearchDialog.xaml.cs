using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        DialogResult parent;

        private bool advancedSearch = false;
        public SearchDialog(ref DialogResult param)
        {
            this.InitializeComponent();

            parent = param;
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string cityName = CityName.Text;
            ComboBoxItem state = (ComboBoxItem)StateName.SelectedItem;
            string stateTag = state.Tag.ToString();

            //string houseNumber;
            //string streetName;
            //string zipCode;

            string address = cityName + ", " + stateTag;

            string addressToGeocode = address;

            BasicGeoposition queryHint = new BasicGeoposition();
            queryHint.Latitude = 47.643;
            queryHint.Longitude = -122.131;
            Geopoint hintPoint = new Geopoint(queryHint);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

            parent.Latitude = result.Locations[0].Point.Position.Latitude;
            parent.Longitude = result.Locations[0].Point.Position.Longitude * -1;

            //DialogFrame.Navigate(typeof(MapPage), parent);

            //DialogFrame.GoBack();

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if(advancedSearch == false)
            {
                Item1.Visibility = Visibility.Visible;
                Item2.Visibility = Visibility.Visible;
                OrLabel.Visibility = Visibility.Collapsed;
                DownArrow.Visibility = Visibility.Collapsed;
                UpArrow.Visibility = Visibility.Visible;
                advancedSearch = true;
            }
            else
            {
                Item1.Visibility = Visibility.Collapsed;
                Item2.Visibility = Visibility.Collapsed;
                OrLabel.Visibility = Visibility.Visible;
                DownArrow.Visibility = Visibility.Visible;
                UpArrow.Visibility = Visibility.Collapsed;

                advancedSearch = false;
            }
            
        }

        private void DownArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AdvnacedButton.Foreground = new SolidColorBrush(Colors.LightGray);
            DownArrow.Foreground = new SolidColorBrush(Colors.LightGray);
            UpArrow.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void DownArrow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AdvnacedButton.Foreground = new SolidColorBrush(Colors.Black);
            DownArrow.Foreground = new SolidColorBrush(Colors.Black);
            UpArrow.Foreground = new SolidColorBrush(Colors.Black);
        }

    }
}
