﻿using System;
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

    public sealed partial class AddLocationDialog : ContentDialog
    {
        double latitude1;
        double longitude1;

        DialogResult parent;
        

        public AddLocationDialog(ref DialogResult param)
        {
            this.InitializeComponent();

            parent = param;

            //TestMethod();

        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            //parent.Latitude = latitude1;
           // parent.Longitude = longitude1;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string cityName = CityName.Text;
            ComboBoxItem state = (ComboBoxItem)StateName.SelectedItem;
            string stateTag = state.Tag.ToString();

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
            parent.Latitude = latitude1;
            parent.Longitude = longitude1;
        }

      

        

    }
}
