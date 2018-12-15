using Entities.ViewModels;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalManager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TodoItemsPage : Page
    {
        public TodoViewModel ViewModel { get; set; }

        public TodoItemsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = (TodoViewModel)e.Parameter;

            ViewModel.NotificationRequested += ViewModel_NotificationRequested;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ViewModel.NotificationRequested -= ViewModel_NotificationRequested;
            base.OnNavigatingFrom(e);
        }

        private void ViewModel_NotificationRequested(object sender, Entities.Models.TodoItem todo)
        {
            foreach (var item in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                //TODO: Eindeutigen Hash erfinden!
                if(item.Id == todo.Title)
                {
                    ToastNotificationManager.CreateToastNotifier().RemoveFromSchedule(item);
                }
            }
            //Nuget Packacke UWP.ToastNotification
            var content = new ToastContent()
            {
                // More about the Launch property at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastcontent
                Launch = "ToastContentActivationParams",

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "TODO ist fällig!"
                            },

                            new AdaptiveText()
                            {
                                 Text = $"{todo.Title}: {todo.Description}"
                            }
                        }
                    }
                },

                Actions = new ToastActionsCustom()
                {
                    Buttons =
                    {
                        // More about Toast Buttons at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastbutton
                        new ToastButton("Anzeigen", todo.Title)
                        {
                            ActivationType = ToastActivationType.Foreground
                        },

                        new ToastButtonDismiss("Abbrechen")
                    }
                }
            };

            var toast = new ScheduledToastNotification(content.GetXml(), todo.TimeDue);
            toast.Id = todo.Title;
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
    }
}
