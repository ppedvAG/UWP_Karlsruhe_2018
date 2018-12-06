using Entities.Interfaces;
using Entities.Models;
using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UniversalManager.Implementations
{
    public class NavigationService : INavigationService
    {
        public static Frame Frame { get; set; }

        public MainViewModel MainViewModelInstance { get; private set; }
        public TodoViewModel TodoViewModelInstance { get; private set; }

        public void Navigate(NavigationTarget name, ModelBase parameter)
        {
            switch (name)
            {
                case NavigationTarget.BookSearch:
                    MainViewModelInstance = parameter as MainViewModel;
                    Frame.Navigate(typeof(BookPage));
                    break;
                case NavigationTarget.TodoItems:
                    TodoViewModelInstance = parameter as TodoViewModel;
                    Frame.Navigate(typeof(TodoPage))
                    break;
                case NavigationTarget.Main:
                    break;
                default:
                    break;
            }
        }
    }
}
