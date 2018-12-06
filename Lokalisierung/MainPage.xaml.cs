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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lokalisierung
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        double pageFontSize = 20;

        public MainPage()
        {
            this.InitializeComponent();


            double pageFontSize = 10;

            double sadsa = pageFontSize;

        }

      
        private void Language_Change_Click(object sender, RoutedEventArgs e)
        {
            if(sender is MenuFlyoutItem item)
            {
                string dateipfad = item.Tag.ToString();
                //Uri - Schemes https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes
                Application.Current.Resources.MergedDictionaries[0].Source = new Uri($"ms-appx:///{dateipfad}");

                //Resource per Code auslesen oder setzen
                //string stringForBread = Application.Current.Resources["bread"].ToString();
                //Application.Current.Resources["bread"] = "Semmel";

                this.Frame.Navigate(typeof(MainPage));
            }
        }

        private void Style_Change_Click(object sender, RoutedEventArgs e)
        {

            if (sender is MenuFlyoutItem item)
            {
                string dateipfad = item.Tag.ToString();
                //Uri - Schemes https://docs.microsoft.com/de-de/windows/uwp/app-resources/uri-schemes
                Application.Current.Resources.MergedDictionaries[1].Source = new Uri($"ms-appx:///{dateipfad}");

                //this.Frame.Navigate(typeof(MainPage));
            }
        }
    }
}
