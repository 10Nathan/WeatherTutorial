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
                    Latitude = 47.604, 
                    Longitude = -122.329

                });
            mapControl.ZoomLevel = 12;
        }

       
        public void MyStyleButton_Click(object sender, RoutedEventArgs e)
        {
            if(mapControl.Style == MapStyle.Aerial)
            {
                mapControl.Style = MapStyle.Road;
                //MyStyleButton.Content = "Aerial";
            }
            else
            {
                mapControl.Style = MapStyle.Aerial;
                //MyStyleButton.Content = "Road";
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
                    Height = 100,
                    Width = 100,
                    BorderBrush = new SolidColorBrush(Windows.UI.Colors.Blue),
                    BorderThickness = new Thickness(5),
                };

                // Center the map over the POI.
                mapControl.Center = snPoint;
                mapControl.ZoomLevel = 14;
                
                // Add XAML to the map.
                mapControl.Children.Add(border);
                MapControl.SetLocation(border, snPoint);
                MapControl.SetNormalizedAnchorPoint(border, new Point(0.5, 0.5));


                ////Start at Microsoft in Redmond, Washington.
                //BasicGeoposition startLocation = new BasicGeoposition() { Latitude = mapResult.Locations[0].Point.Position.Latitude, Longitude = mapResult.Locations[0].Point.Position.Longitude };

                //// End at the city of Seattle, Washington.
                //BasicGeoposition endLocation = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };


                //// Get the route between the points.
                //MapRouteFinderResult routeResult =
                //        await MapRouteFinder.GetDrivingRouteAsync(
                //        new Geopoint(startLocation),
                //        new Geopoint(endLocation),
                //        MapRouteOptimization.Time,
                //        MapRouteRestrictions.None);

                //if (routeResult.Status == MapRouteFinderStatus.Success)
                //{
                //    // Use the route to initialize a MapRouteView.
                //    viewOfRoute = new MapRouteView(routeResult.Route);
                //    viewOfRoute.RouteColor = Colors.Yellow;
                //    viewOfRoute.OutlineColor = Colors.Black;

                //    // Add the new MapRouteView to the Routes collection
                //    // of the MapControl.
                //    mapControl.Routes.Add(viewOfRoute);

                //    // Fit the MapControl to the route.
                //    await mapControl.TrySetViewBoundsAsync(
                //            routeResult.Route.BoundingBox,
                //            null,
                //            Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                //}
            }

        }

        private async void searchRoute()
        {
            //double lat = Math.Round(dataResult.Latitude, 3);
            //double lng = Math.Round(dataResult.Longitude, 3);

            //double lat = 35.2468;
            //double lng = 91.7337;

            //if (dataResult.Latitude != 0 && dataResult.Longitude != 0)
            //{
            //    //Start at Microsoft in Redmond, Washington.
            //    BasicGeoposition startLocation = new BasicGeoposition() { Latitude = lat, Longitude = lng };

            //    // End at the city of Seattle, Washington.
            //    BasicGeoposition endLocation = new BasicGeoposition() { Latitude = 47.604, Longitude = -122.329 };


            //    // Get the route between the points.
            //    MapRouteFinderResult routeResult =
            //            await MapRouteFinder.GetDrivingRouteAsync(
            //            new Geopoint(startLocation),
            //            new Geopoint(endLocation),
            //            MapRouteOptimization.Time,
            //            MapRouteRestrictions.None);

            //    if (routeResult.Status == MapRouteFinderStatus.Success)
            //    {
            //        // Use the route to initialize a MapRouteView.
            //        MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
            //        viewOfRoute.RouteColor = Colors.Yellow;
            //        viewOfRoute.OutlineColor = Colors.Black;

            //        // Add the new MapRouteView to the Routes collection
            //        // of the MapControl.
            //        MapControl.Routes.Add(viewOfRoute);

            //        // Fit the MapControl to the route.
            //        await MapControl.TrySetViewBoundsAsync(
            //                routeResult.Route.BoundingBox,
            //                null,
            //                Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            //    }

            }
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    DialogResult message = e.Parameter as DialogResult;

        //    if (message != null)
        //    {
        //        dataResult = message;
        //        searchRoute();
        //    }


        //}

    

}
