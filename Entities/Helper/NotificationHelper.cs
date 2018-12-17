using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class NotificationHelper
    {
        private static INotificationService _service;

        public static INotificationService Service
        {
            get
            {
                if (_service != null)
                    return _service;
                throw new ArgumentException("You have to register your service first");
            }
        }

        protected NotificationHelper()
        {

        }

        public static void RegisterService(INotificationService service)
        {
            _service = service;
        }
    }
}
