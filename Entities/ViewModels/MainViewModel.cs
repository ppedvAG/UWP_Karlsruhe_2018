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
        public ICommand SaveDataCommand { get; set; }


        public MainViewModel()
        {
            GoToTodoItemsCommand = new DelegateCommand(p => GoToTodoItems());
            GoToBookSearchCommand = new DelegateCommand(p => GoToBookSearch());
            SaveDataCommand = new DelegateCommand(p => ServicesHelper.DataService?.SaveData(TodoItemsManager.TodoItems, TodoItem.Last_TODO_ID));

            var data = ServicesHelper.DataService?.LoadData();
            if(data != null)
            {
                TodoItemsManager.TodoItems = data.Value.todoitems;
                TodoItem.Last_TODO_ID = data.Value.lastId;
            }
        }

        public void GoToTodoItems()
        {
            ServicesHelper.NavigationService.Navigate(Interfaces.NavigationTarget.TodoItems, new TodoViewModel());
        }

        public void GoToBookSearch()
        {
            ServicesHelper.NavigationService.Navigate(Interfaces.NavigationTarget.BookSearch, new BookSearchViewModel());
        }
    }
}
