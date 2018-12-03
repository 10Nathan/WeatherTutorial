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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    
    public sealed partial class TravelPage : Page
    {

        static List<ListViewItem> trips = new List<ListViewItem>();
        string newTrip;

        public TravelPage()
        {
            this.InitializeComponent();
        }

        static int count = 1;

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async void Trip_Click(object sender, RoutedEventArgs e)
        {
            AddTrip trip = new AddTrip();

            EnterTrip tripDialog = new EnterTrip(ref trip);
            var result = await tripDialog.ShowAsync();


            ListViewItem newButton = new ListViewItem();
            //ListViewItem CopynewButton = new ListViewItem();

            if (result == ContentDialogResult.Primary)
            {
                //newButton.TabIndex = count;
                //newButton.PointerReleased += SubscribeButton_Click;

                trips.Add(newButton);

                newButton.Content = trip.tripName;
                //CopynewButton.Content = trip.tripName;
                //count++;



                listView.Items.Add(newButton);
            }

        }


        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem SelectedItem = sender as ListViewItem;

            DependencyObject dep = (DependencyObject)e.OriginalSource;

            string item = dep.ToString();

            int a = 5;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            int b = 5;
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {

            int c = 123;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)e.AddedItems?.FirstOrDefault();

            string strItem = (string)item.Content;
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
    }
}
