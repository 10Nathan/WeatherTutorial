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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace WeatherTutorial
{
    public sealed partial class EnterTrip : ContentDialog
    {
        List<string> currentTrips;
        AddTrip newTrip;
        public EnterTrip(ref AddTrip trip)
        {
            this.InitializeComponent();

            newTrip = trip;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            //for(int i = 0; i < currentTrips.Count; i++)
            //{
            //    if(tripName.Text == currentTrips[i])
            //    {
            //        int a = 12;
            //    }
            //}

            newTrip.tripName = tripName.Text;


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
