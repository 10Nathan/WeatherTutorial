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
using Windows.Services.Maps;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : Page
    {

        MainPage mainPage = new MainPage();
        DialogResult dataResult = new DialogResult();
        MapRouteView viewOfRoute;
        public MapPage()
        {
            this.InitializeComponent();
            mapControl.Loaded += MapControl_Loaded;
            mapControl.MapTapped += MapControl_MapTapped;


        }


        private void MapControl_Loaded(Object sender, RoutedEventArgs e)
        {
            mapControl.Center =
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = 37.604,
                    Longitude = -100.329

                });
            mapControl.ZoomLevel = 5.5;
        }


        public void MyStyleButton_Click(object sender, RoutedEventArgs e)
        {
            if (mapControl.Style == MapStyle.Aerial)
            {
                mapControl.Style = MapStyle.Road;
            }
            else
            {
                mapControl.Style = MapStyle.Aerial;
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

        public void ChangeMap()
        {
            this.mapControl.Style = MapStyle.Aerial3DWithRoads;
        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {

            SearchDialog dialog = new SearchDialog(ref dataResult);
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (viewOfRoute != null)
                {
                    mapControl.Routes.Remove(viewOfRoute);
                }

                double a = dataResult.Latitude;


                string address = dataResult.address;

                if (dataResult.fullAddress == true)
                {



                    string addressToGeocode = address;

                    BasicGeoposition queryHint = new BasicGeoposition();
                    queryHint.Latitude = 47.643;
                    queryHint.Longitude = -122.131;
                    Geopoint hintPoint = new Geopoint(queryHint);

                    MapLocationFinderResult mapResult = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

                    dataResult.Latitude = mapResult.Locations[0].Point.Position.Latitude;
                    dataResult.Longitude = mapResult.Locations[0].Point.Position.Longitude * -1;


                    // Specify a known location.
                    BasicGeoposition snPosition = new BasicGeoposition { Latitude = mapResult.Locations[0].Point.Position.Latitude, Longitude = mapResult.Locations[0].Point.Position.Longitude };
                    Geopoint snPoint = new Geopoint(snPosition);

                    // Create a XAML border.
                    Border border = new Border
                    {
                        CornerRadius = new CornerRadius(10),
                        Height = 100,
                        Width = 100,
                        BorderBrush = new SolidColorBrush(Windows.UI.Colors.Orange),
                        BorderThickness = new Thickness(2),
                    };

                    // Center the map over the POI.
                    mapControl.Center = snPoint;
                    mapControl.ZoomLevel = 14;

                    // Add XAML to the map.
                    mapControl.Children.Add(border);
                    MapControl.SetLocation(border, snPoint);
                    MapControl.SetNormalizedAnchorPoint(border, new Point(0.5, 0.5));
                }
                else
                {
                    var messageDialog = new MessageDialog("Location not found. All fields are required.");
                    await messageDialog.ShowAsync();
                }

            }

        }

    }

}
