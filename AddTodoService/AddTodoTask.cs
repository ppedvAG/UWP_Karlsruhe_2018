//using Entities.Models;
//using Newtonsoft.Json;
using Entities.Models;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;

namespace AddTodoService
{
    //Damit der App-Service funktioniert, muss in der UniversalManager-App
    //dieses Projekt referenziert werden und der Einstiegspunkt unter Properties->Manifest->Declarations definiert werden
    public sealed class AddTodoTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        AppServiceConnection appServiceconnection;
        const string Local_Settings_Todo = "CurrentTodos";
        const string Last_Todo_ID = "LastTodoID";

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += OnTaskCanceled;

            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;

            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            var messageDeferral = args.GetDeferral();

            ValueSet message = args.Request.Message;
            ValueSet returnData = new ValueSet();
            try
            {
                string command = message["Command"] as string;
                string todoTitle = message["Title"] as string;

                if (command == "Add Todo" && todoTitle.Length > 0)
                {

                    ObservableCollection<TodoItem> todos = new ObservableCollection<TodoItem>();
                    bool settingsCreated = false;
                    int lastID = 0;
                    if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Local_Settings_Todo, out object json))
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        todos = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(json.ToString(), settings);
                        settingsCreated = true;
                        if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Last_Todo_ID, out object last))
                        {
                            lastID = (int)last;
                        }
                    }
                    lastID++;
                    TodoItem newTodo = new TodoItem(lastID, todoTitle, "...");
                    todos.Add(newTodo);
                    if (settingsCreated)
                    {
                        ApplicationData.Current.LocalSettings.Values[Local_Settings_Todo] = JsonConvert.SerializeObject(todos);
                        ApplicationData.Current.LocalSettings.Values[Last_Todo_ID] = lastID;
                    }
                    else
                    {
                        ApplicationData.Current.LocalSettings.Values.Add(Local_Settings_Todo, JsonConvert.SerializeObject(todos));
                        ApplicationData.Current.LocalSettings.Values.Add(Last_Todo_ID, lastID);
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
                                Text = "TODO wurde erstellt!"
                            },

                            new AdaptiveText()
                            {
                                 Text = $"{newTodo.Title}: {newTodo.Description}"
                            }
                        }
                            }
                        },

                        Actions = new ToastActionsCustom()
                        {
                            Buttons =
                            {
                                // More about Toast Buttons at https://docs.microsoft.com/dotnet/api/microsoft.toolkit.uwp.notifications.toastbutton
                                new ToastButton("Anzeigen", newTodo.ID.ToString())
                                {
                                    ActivationType = ToastActivationType.Foreground
                                },

                                new ToastButtonDismiss("Abbrechen")
                            }
                        }
                    };

                    var toast = new ToastNotification(content.GetXml());
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                    returnData.Add("Status", "OK");
                    await args.Request.SendResponseAsync(returnData);
                }
            }
            catch (Exception e)
            {
                returnData.Add("Status", e.Message);
                await args.Request.SendResponseAsync(returnData);
                return;
            }
            finally
            {
                messageDeferral?.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (_deferral != null)
            {
                _deferral.Complete();
                _deferral = null;
            }

            if (appServiceconnection != null)
            {
                appServiceconnection.Dispose();
                appServiceconnection = null;
            }
        }
    }
}