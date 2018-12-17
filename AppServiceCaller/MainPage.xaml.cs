using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.AppService;
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

namespace AppServiceCaller
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            tbStatus.Items.Clear();

            using (var connection = new AppServiceConnection())
            {
                connection.AppServiceName = "com.ppedv.todos";
                //kann im UniversalManager-Projekt mittels "Package.Current.Id.FamilyName" ermittelt werden
                connection.PackageFamilyName = "548799e1-66bf-474c-bb3a-c8a8e92c133f_effe681hq0sj4";
                AppServiceConnectionStatus status = await connection.OpenAsync();

                switch (status)
                {
                    case AppServiceConnectionStatus.Success:
                        tbStatus.Items.Add("Verbindung zum Webservice hergestellt!");
                        break;

                    case AppServiceConnectionStatus.AppNotInstalled:
                        tbStatus.Items.Add("Nicht installiert!");
                        return;

                    case AppServiceConnectionStatus.AppUnavailable:
                        tbStatus.Items.Add("Nicht verfügbar!");
                        return;

                    case AppServiceConnectionStatus.AppServiceUnavailable:
                        tbStatus.Items.Add("App Service nicht verfügbar!");
                        return;
                    case AppServiceConnectionStatus.Unknown:
                        tbStatus.Items.Add("Unbekannter Fehler!");
                        return;
                }

                var inputs = new ValueSet();
                inputs.Add("Command", "Add Todo");
                inputs.Add("Title", textboxTitle.Text);
                AppServiceResponse response = await connection.SendMessageAsync(inputs);

                if (response.Status == AppServiceResponseStatus.Success)
                {
                    tbStatus.Items.Add("Todo wurde durch App-Service erstellt!");
                    return;
                }

                switch (response.Status)
                {
                    case AppServiceResponseStatus.Failure:
                        tbStatus.Items.Add("Fehler!");
                        return;
                    case AppServiceResponseStatus.ResourceLimitsExceeded:
                        tbStatus.Items.Add("ResourceLimit überstiegen!");
                        return;
                    case AppServiceResponseStatus.Unknown:
                        tbStatus.Items.Add("Unbekannter Fehler beim Senden!");
                        return;
                }
            }
        }
    }
}