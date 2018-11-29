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
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {
        public MapPage()
        {
            this.InitializeComponent();
            MapControl.Loaded += MapControl_Loaded;
            MapControl.MapTapped += MapControl_MapTapped;
        }


        private void MapControl_Loaded(Object sender, RoutedEventArgs e)
        {
            MapControl.Center =
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = 47.604, 
                    Longitude = -122.329

                });
            MapControl.ZoomLevel = 12;
        }

       
        private void MyStyleButton_Click(object sender, RoutedEventArgs e)
        {
            if(MapControl.Style == MapStyle.Aerial)
            {
                MapControl.Style = MapStyle.Road;
                MyStyleButton.Content = "Aerial";
            }
            else
            {
                MapControl.Style = MapStyle.Aerial;
                MyStyleButton.Content = "Road";
            }
        }

        private async void MapControl_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            var tappedGeoPosition = args.Location.Position;
            string status = $"Map tapped at Latitude: {tappedGeoPosition.Latitude}" +
                            $"Longitude: {tappedGeoPosition.Longitude}";
            var messageDialog = new MessageDialog(status);
            await messageDialog.ShowAsync();
        }
    }
}
