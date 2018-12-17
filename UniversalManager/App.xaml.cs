using Entities.Helper;
using Entities.Models;
using Entities.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalManager.Implementations;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
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

namespace UniversalManager
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            NavigationHelper.RegisterService(new NavigationService());
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            
        }

        public const string Local_Settings_Todo = "CurrentTodos";
        public const string Last_Todo_ID = "LastTodoID";


        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            PrepareLaunch(e);
        }

        public void PrepareLaunch(LaunchActivatedEventArgs e = null)
        {
            //Todos laden
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Local_Settings_Todo, out object json))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                TodoItemsManager.TodoItems = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(json.ToString(), settings);
            }
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Last_Todo_ID, out object lastID))
            {
                TodoItem.Last_TODO_ID = (int)lastID;
            }

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e != null && e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e == null || e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    NavigationService.Frame = rootFrame;
                    NavigationHelper.Service.Navigate(Entities.Interfaces.NavigationTarget.Main, new MainViewModel());

                    //rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            PrepareLaunch();

            if(args is ToastNotificationActivatedEventArgs toastargs && int.TryParse(toastargs.Argument, out int todoID))
            {
                TodoViewModel model = new TodoViewModel(TodoItemsManager.TodoItems.FirstOrDefault(t => t.ID == todoID));
                NavigationHelper.Service.Navigate(Entities.Interfaces.NavigationTarget.TodoItems, model);
            }
            base.OnActivated(args);
        }
    }
}