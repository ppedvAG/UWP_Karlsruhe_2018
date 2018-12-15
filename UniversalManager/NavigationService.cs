using Entities.Interfaces;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalManager.Views;
using Windows.UI.Xaml.Controls;

namespace UniversalManager.Implementations
{
    public class NavigationService : INavigationService
    {
        public static Frame Frame { get; set; }

        public void Navigate(NavigationTarget name, ModelBase parameter)
        {
            switch (name)
            {
                case NavigationTarget.BookSearch:
                    Frame.Navigate(typeof(BookSearchPage), parameter);
                    break;
                case NavigationTarget.TodoItems:
                    Frame.Navigate(typeof(TodoItemsPage), parameter);
                    break;
                case NavigationTarget.Main:
                    Frame.Navigate(typeof(MainPage), parameter);
                    break;
                default:
                    break;
            }
        }
    }
}
