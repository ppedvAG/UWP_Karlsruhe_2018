using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class NavigationHelper
    {

        protected NavigationHelper(INavigationService service)
        {
            
        }

        public static void RegisterService(INavigationService service)
        {
            _service = service;
        }

        private static INavigationService _service;

        public static INavigationService Service
        {
            get
            {
                if (_service != null)
                    return _service;
                throw new ArgumentException("You have to register your service first");
            }
        }
    }
}