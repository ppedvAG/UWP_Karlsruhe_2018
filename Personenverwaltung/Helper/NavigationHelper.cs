using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Personenverwaltung.Helper
{
    public static class NavigationHelper
    {
        public enum Pages { Details, PersonCollection }

        public static Frame _rootFrame;

        public static Pages CurrentPage;

        public static void Navigate(Pages page, object parameter)
        {
            switch (page)
            {
                case Pages.Details:
                    _rootFrame.Navigate(typeof(PersonDetailPage), parameter);
                    break;
                case Pages.PersonCollection:
                    _rootFrame.Navigate(typeof(PersonCollectionPage), parameter);
                    break;
                default:
                    break;
            }
        }
    }
}
