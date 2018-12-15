//using Entities.Models;
//using Newtonsoft.Json;
using Entities.Models;
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

namespace AddTodoService
{
    public sealed class AddTodoTask : IBackgroundTask
    {
        BackgroundTaskDeferral _deferral;
        AppServiceConnection appServiceconnection;
        const string Local_Settings_Todo = "CurrentTodos";

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            //Take a service deferral so the service isn't terminated
            _deferral = taskInstance.GetDeferral();

            taskInstance.Canceled += OnTaskCanceled;

            var details = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            appServiceconnection = details.AppServiceConnection;

            //Listen for incoming app service requests
            appServiceconnection.RequestReceived += OnRequestReceived;
        }

        private async void OnRequestReceived(AppServiceConnection sender, AppServiceRequestReceivedEventArgs args)
        {
            //var deferal = args.GetDeferral();

            

            //var result = new ValueSet();
            ////result.Add("result", randomNumberGenerator.Next(minValue, maxValue));
            //result.Add("result", 5);

            ////Send the response
            //await args.Request.SendResponseAsync(result);


            //deferal?.Complete();

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
                    if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Local_Settings_Todo, out object json))
                    {
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.TypeNameHandling = TypeNameHandling.Objects;
                        todos = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(json.ToString(), settings);
                        settingsCreated = true;
                    }
                    todos.Add(new TodoItem(todoTitle, "...", new NotifyProperty<string>(".."), DateTime.Now.AddDays(7), false));
                    if (settingsCreated)
                    {
                        ApplicationData.Current.LocalSettings.Values[Local_Settings_Todo] = JsonConvert.SerializeObject(todos);
                    }
                    else
                    {
                        ApplicationData.Current.LocalSettings.Values.Add(Local_Settings_Todo, JsonConvert.SerializeObject(todos));
                    }
                    returnData.Add("Status", "OK");
                    await args.Request.SendResponseAsync(returnData); // Return the data to the caller.
                }
            }
            catch (Exception e)
            {
                returnData.Add("Status", e.Message);
                await args.Request.SendResponseAsync(returnData); // Return the data to the caller.
                return;
            }
            finally
            {
                // Complete the deferral so that the platform knows that we're done responding to the app service call.
                // Note for error handling: this must be called even if SendResponseAsync() throws an exception.
                messageDeferral?.Complete();
            }
        }


        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            if (_deferral != null)
            {
                //Complete the service deferral
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