using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Helper
{
    public class DataHelper
    {
        private static IDataService _service;

        public static IDataService Service
        {
            get
            {
                if (_service != null)
                    return _service;
                throw new ArgumentException("You have to register your service first");
            }
        }

        protected DataHelper()
        {

        }

        public static void RegisterService(IDataService service)
        {
            _service = service;
        }
    }
}
