using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public static class ServicesHelper
    {

        private static INavigationService _navigationService;
        public static INavigationService NavigationService => _navigationService ?? throw new ArgumentException("You have to register your NavigationService first!");

        private static IDataService _dataService;
        public static IDataService DataService => _dataService ?? throw new ArgumentException("You have to register your DataService first!");

        private static INotificationService _notificationService;
        public static INotificationService NotificationService => _notificationService ?? throw new Exception("You have to register your NotificationService first!");

        public static void RegisterNavigationService(INavigationService service)
        {
            _navigationService = service;
        }

        public static void RegisterDataService(IDataService service)
        {
            _dataService = service;
        }

        public static void RegisterNotificationService(INotificationService service)
        {
            _notificationService = service;
        }

    }
}