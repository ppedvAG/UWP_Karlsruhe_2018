using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Entities.ViewModels
{
    public class MainViewModel : ModelBase
    {

        public ICommand GoToTodoItemsCommand { get; set; }
        public ICommand GoToBookSearchCommand { get; set; }

        public MainViewModel()
        {
            GoToTodoItemsCommand = new DelegateCommand(p => GoToTodoItems());
            GoToBookSearchCommand = new DelegateCommand(p => GoToBookSearch());
        }

        public void GoToTodoItems()
        {
            
                
            NavigationHelper.Service.Navigate(Interfaces.NavigationTarget.TodoItems, new TodoViewModel());
        }

        public void GoToBookSearch()
        {
            NavigationHelper.Service.Navigate(Interfaces.NavigationTarget.BookSearch, new BookSearchViewModel());
        }
    }
}
