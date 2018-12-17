using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UniversalManager.Implementations;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public string FamilyName { get; set; }

        public MainPage()
        {
            this.InitializeComponent();

            FamilyName = Package.Current.Id.FamilyName;

            NavigationService.Frame = mainFrame;
            Application.Current.Suspending += Current_Suspending;
        }

        private void Current_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {

            ViewModel.SaveDataCommand.Execute(null);
           
            //Wenn Current_Suspending fertig ist, wird das Programm beendet und Thread-Code wird nicht mehr zu Ende geführt
            Task t = Task.Run(async () =>
            {
                //var handle = await ApplicationData.Current.LocalFolder.CreateFileAsync("Todos.json", CreationCollisionOption.ReplaceExisting);
            });
            Task.WaitAll(t);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (MainViewModel)e.Parameter;
            base.OnNavigatedTo(e);
        }

        private void NavigationViewItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is NavigationViewItem item)
            {
                switch (item.Tag.ToString())
                {
                    case "Books":
                        //Zur Buchseite navigieren
                        ViewModel.GoToBookSearchCommand.Execute(null);
                        break;
                    case "Todos":
                        //Zur Todosseite
                        ViewModel.GoToTodoItemsCommand.Execute(null);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
