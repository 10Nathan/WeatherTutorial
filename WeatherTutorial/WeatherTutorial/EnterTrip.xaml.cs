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
        StoredTrips newTrip;
        public EnterTrip(ref StoredTrips trip)
        {
            this.InitializeComponent();

            newTrip = trip;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            newTrip.allFields = true;

            ComboBoxItem state = (ComboBoxItem)StartingStateName.SelectedItem;
            string stateTag = null;

            if (state != null)
            {
                stateTag = state.Tag.ToString();
            }
            else
            {
                newTrip.allFields = false;
            }

            newTrip.tripName = tripName.Text;

            
            if(tripName.Text.Trim() == "")
            {
                newTrip.allFields = false;
            }

            newTrip.startingPoint = startingPoint.Text + ", " + stateTag;

            if(startingPoint.Text.Trim() == "")
            {
                newTrip.allFields = false;
            }

            state = (ComboBoxItem)DestinationStateName.SelectedItem;

            if (state != null)
            {
                stateTag = state.Tag.ToString();
            }
            else
            {
                newTrip.allFields = false;
            }

            newTrip.destination = destination.Text + ", " + stateTag;

            if(destination.Text.Trim() == "")
            {
                newTrip.allFields = false;
            }


        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
