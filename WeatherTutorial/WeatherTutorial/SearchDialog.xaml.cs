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
   
    public sealed partial class SearchDialog : ContentDialog
    {
        private bool advancedSearch = false;
        public SearchDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if(advancedSearch == false)
            {
                Item1.Visibility = Visibility.Visible;
                Item2.Visibility = Visibility.Visible;
                OrLabel.Visibility = Visibility.Collapsed;
                advancedSearch = true;
            }
            else
            {
                Item1.Visibility = Visibility.Collapsed;
                Item2.Visibility = Visibility.Collapsed;
                OrLabel.Visibility = Visibility.Visible;
                advancedSearch = false;
            }
            
        }
    }
}
