using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Services.Maps;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WeatherPage : Page
    {
        string Lat, Lon;
        DialogResult dataResult = new DialogResult();
        public WeatherPage()
        {
            this.InitializeComponent();
            //HideStatusBar();
        }

        //private void HideStatusBar()
        //{
        //    if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 1, 0))
        //    {
        //        var titleBar = ApplicationView.GetForCurrentView().TitleBar;

        //    }
        //}

        private async void btnGetWeather_Click(object sender, RoutedEventArgs e)
        {
            progressRing.IsActive = true;
            var geoLocator = new Geolocator();
            geoLocator.DesiredAccuracy = PositionAccuracy.High;
            Geoposition pos = await geoLocator.GetGeopositionAsync();

            Lat = pos.Coordinate.Point.Position.Latitude.ToString();
            Lon = pos.Coordinate.Point.Position.Longitude.ToString();

            var data = await Helper.Helper.GetWeather(Lat, Lon);
            if (data != null)
            {
                txtCity.Text = $"{data.name}, {data.sys.country}";
                lastUpdate.Text = $"Last updated : {DateTime.Now.ToString("dd MMMM yyyy HH:mm")}";

                BitmapImage image = new BitmapImage(new Uri($"http://openweathermap.org/img/w/{data.weather[0].icon}.png", UriKind.Absolute));
                imgWeather.Source = image;

                txtDescription.Text = $"{data.weather[0].description}";
                txtHumidity.Text = $"Humidity : {data.main.humidity}";
                
                txtFrh.Text = $"{data.main.temp} ℉";
            }
            progressRing.IsActive = false;
        }


        private async void AddLocation_Click(object sender, RoutedEventArgs e)
        {

            AddLocationDialog dialog = new AddLocationDialog(ref dataResult);
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {

                double a = dataResult.Latitude;


                string address = dataResult.address;

                string addressToGeocode = address;

                BasicGeoposition queryHint = new BasicGeoposition();
                queryHint.Latitude = 47.643;
                queryHint.Longitude = -122.131;
                Geopoint hintPoint = new Geopoint(queryHint);

                MapLocationFinderResult mapResult = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

                dataResult.Latitude = mapResult.Locations[0].Point.Position.Latitude;
                dataResult.Longitude = mapResult.Locations[0].Point.Position.Longitude * -1;
                Lat = dataResult.Latitude.ToString();
                Lon = dataResult.Longitude.ToString();

                var data = await Helper.Helper.GetWeather(Lat, Lon);
                if (data != null)
                {
                    txtCity.Text = $"{data.name}, {data.sys.country}";
                    lastUpdate.Text = $"Last updated : {DateTime.Now.ToString("dd MMMM yyyy HH:mm")}";

                    BitmapImage image = new BitmapImage(new Uri($"http://openweathermap.org/img/w/{data.weather[0].icon}.png", UriKind.Absolute));
                    imgWeather.Source = image;

                    txtDescription.Text = $"{data.weather[0].description}";
                    txtHumidity.Text = $"Humidity : {data.main.humidity}%";
                    txtFrh.Text = $"{data.main.temp} ℉";
                }
                progressRing.IsActive = false;

            }
        }



    }
}




