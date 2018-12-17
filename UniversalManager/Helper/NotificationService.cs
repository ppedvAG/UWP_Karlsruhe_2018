using Entities.Interfaces;
using Entities.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace UniversalManager
{
    public class NotificationService : INotificationService
    {
        public void CreateNotification(TodoItem todo)
        {
            foreach (var item in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                if (item.Id == todo.ID.ToString())
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
                        new ToastButton("Anzeigen", todo.ID.ToString())
                        {
                            ActivationType = ToastActivationType.Foreground
                        },

                        new ToastButtonDismiss("Abbrechen")
                    }
                }
            };

            var toast = new ScheduledToastNotification(content.GetXml(), todo.TimeDue);
            toast.Id = todo.ID.ToString();
            ToastNotificationManager.CreateToastNotifier().AddToSchedule(toast);
        }
    }
}
