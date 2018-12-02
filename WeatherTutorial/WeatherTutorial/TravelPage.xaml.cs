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
        public TravelPage()
        {
            this.InitializeComponent();
        }

        static int count = 1;

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Trip_Click(object sender, RoutedEventArgs e)
        {
            RadioButton newButton = new RadioButton();
            newButton.Width = 5000;

            newButton.Click += SubscribeButton_Click;

            newButton.Content = "Trip: " + count;
            count++;

            listView.Items.Add(newButton);
        }


        private async void SubscribeButton_Click(object sender, RoutedEventArgs e)
        {
            int a = 5;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            int b = 5;
        }
    }
}
