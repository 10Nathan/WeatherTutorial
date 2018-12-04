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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    
    public sealed partial class TravelPage : Page
    {

        static List<ListViewItem> trips = new List<ListViewItem>();
        static List<StoredTrips> storedTrips = new List<StoredTrips>();

        public string path;
        public SQLite.Net.SQLiteConnection connection;

        string newTrip;
        private bool clicked = false;
        MapRouteView viewOfRoute;

        public TravelPage()
        {
            this.InitializeComponent();

            path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
            connection = new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path);

            connection.CreateTable<AddTrip>();
            populateListView();

        }

        public void populateListView()
        {
            
            var itemsToAdd = connection.Table<AddTrip>();
            //string name;
            //string dest;
            //string start;
            //AddTrip addTrp = new AddTrip();

            foreach(var addTrip in itemsToAdd)
            {
                StoredTrips newTrip = new StoredTrips();

                newTrip.startingPoint = addTrip.startingPoint;
                newTrip.destination = addTrip.destination;
                newTrip.tripName = addTrip.tripName;

                storedTrips.Add(newTrip);

                ListViewItem newButton = new ListViewItem();

                trips.Add(newButton);

                newButton.Content = addTrip.tripName;

                listView.Items.Add(newButton);
            }
        } 

        static int count = 1;

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Trip_Click(object sender, RoutedEventArgs e)
        {
            AddTrip trip = new AddTrip();
            StoredTrips newTrip = new StoredTrips();

            EnterTrip tripDialog = new EnterTrip(ref newTrip);
            var result = await tripDialog.ShowAsync();


            ListViewItem newButton = new ListViewItem();


            if (result == ContentDialogResult.Primary)
            {

                //stored the entered trip with startingPoint and destination
                //newTrip.tripName = trip.tripName;
                
                trip.tripName = newTrip.tripName;
                
                storedTrips.Add(newTrip);

                connection.Insert(new AddTrip()
                {
                    tripName = newTrip.tripName,
                    destination = newTrip.destination,
                    startingPoint = newTrip.tripName
                });

                trips.Add(newButton);
                newButton.Content = trip.tripName;
                listView.Items.Add(newButton);
            }

        }


        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem SelectedItem = sender as ListViewItem;

            DependencyObject dep = (DependencyObject)e.OriginalSource;

            string item = dep.ToString();

            //int a = 5;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            //int b = 5;
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;

            //string items = listView.SelectedItem.ToString();

          //  int c = 123;

            clicked = false;
        }

        private async void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)e.AddedItems?.FirstOrDefault();

            if (clicked != true)
            {
                if (mapControl.Visibility != Visibility.Visible)
                {
                    if (item != null)
                    {
                        if (viewOfRoute != null)
                        {
                            mapControl.Routes.Remove(viewOfRoute);
                        }

                        string strItem = (string)item.Content;

                        //listView.Items.Clear();

                        backButton.IsEnabled = true;
                        listView.Visibility = Visibility.Collapsed;
                        //listView.IsItemClickEnabled = false;
                        //listView.IsEnabled = false;
                        mapControl.Visibility = Visibility.Visible;


                        int i;
                        string addressToGeocode = "";
                        string DestinationAddress = "";

                        for (i = 0; i < storedTrips.Count; i++)
                        {
                            if (strItem == storedTrips[i].tripName)
                            {
                                addressToGeocode = storedTrips[i].startingPoint;
                                DestinationAddress = storedTrips[i].destination;
                            }
                        }




                        BasicGeoposition queryHint = new BasicGeoposition();
                        queryHint.Latitude = 47.643;
                        queryHint.Longitude = -122.131;
                        Geopoint hintPoint = new Geopoint(queryHint);

                        MapLocationFinderResult StartingmapResult = await MapLocationFinder.FindLocationsAsync(addressToGeocode, hintPoint, 3);

                        MapLocationFinderResult DestinationmapResult = await MapLocationFinder.FindLocationsAsync(DestinationAddress, hintPoint, 3);


                        //// Specify a known location.
                        //BasicGeoposition snPosition = new BasicGeoposition { Latitude = StartingmapResult.Locations[0].Point.Position.Latitude, Longitude = StartingmapResult.Locations[0].Point.Position.Longitude };
                        //Geopoint snPoint = new Geopoint(snPosition);


                        // Start at Microsoft in Redmond, Washington.
                        BasicGeoposition startLocation = new BasicGeoposition() { Latitude = StartingmapResult.Locations[0].Point.Position.Latitude, Longitude = StartingmapResult.Locations[0].Point.Position.Longitude };

                        // End at the city of Seattle, Washington.
                        BasicGeoposition endLocation = new BasicGeoposition() { Latitude = DestinationmapResult.Locations[0].Point.Position.Latitude, Longitude = DestinationmapResult.Locations[0].Point.Position.Longitude };


                        // Get the route between the points.
                        MapRouteFinderResult routeResult =
                              await MapRouteFinder.GetDrivingRouteAsync(
                              new Geopoint(startLocation),
                              new Geopoint(endLocation),
                              MapRouteOptimization.Time,
                              MapRouteRestrictions.None);

                        if (routeResult.Status == MapRouteFinderStatus.Success)
                        {
                            // Use the route to initialize a MapRouteView.
                            viewOfRoute = new MapRouteView(routeResult.Route);
                            viewOfRoute.RouteColor = Colors.Yellow;
                            viewOfRoute.OutlineColor = Colors.Black;

                            // Add the new MapRouteView to the Routes collection
                            // of the MapControl.
                            mapControl.Routes.Add(viewOfRoute);

                            // Fit the MapControl to the route.
                            await mapControl.TrySetViewBoundsAsync(
                                  routeResult.Route.BoundingBox,
                                  null,
                                  Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
                        }

                        if (mapControl.Visibility != Visibility.Collapsed)
                        {
                            relaeseItems();
                        }

                        if (mapControl.Visibility == Visibility.Collapsed && backButton.IsEnabled == false)
                        {
                            recreateList();
                        }

                    }
                }
            }
            else
            {
                clicked = false;
            }

        }


        private void relaeseItems()
        {
            
        }

        private void recreateList()
        {
           
        }


        private void Page_Loading(FrameworkElement sender, object args)
        {
            if(trips.Count != 0)
            {
                for(int i = 0; i < trips.Count; i++)
                {
                    listView.Items.Add(trips[i]);
                }
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            listView.Items.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clicked = true;
            //listView.SelectedItems.Clear();

            backButton.IsEnabled = false;
            mapControl.Visibility = Visibility.Collapsed;

            //listView.Items.Clear();

            //if (trips.Count != 0)
            //{
            //    for (int i = 0; i < trips.Count; i++)
            //    {
            //        listView.Items.Add(trips[i]);
            //    }
            //}

            mapControl.Visibility = Visibility.Collapsed;
            listView.Visibility = Visibility.Visible;

            
        }
    }
}
