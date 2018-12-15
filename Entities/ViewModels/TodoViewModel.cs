using Entities.Helper;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Entities.ViewModels
{
    public class TodoViewModel : ModelBase
    {
        public event EventHandler<TodoItem> NotificationRequested;

        public ObservableCollection<TodoItem> Todos => TodoItemsManager.TodoItems;

        public int Count => TodoItemsManager.TodoItems.Count;

        private TodoItem _selectedTodo;
        public TodoItem SelectedTodo
        {
            get { return _selectedTodo; }
            set { SetValue(ref _selectedTodo, value); }
        }

        public ICommand CreateTodoCommand { get; set; }

        public TodoViewModel(Book book) : this()
        {
            TodoItem neuesTodo = new TodoItem();
            neuesTodo.Title = $"{book.Title} lesen!";
            neuesTodo.Description = book.Description;
            neuesTodo.TimeDue = DateTime.Now + TimeSpan.FromDays(10);
            Todos.Add(neuesTodo);
            SelectedTodo = neuesTodo;
            //base.OnPropertyChanged(nameof(Todos));
            //base.OnPropertyChanged(nameof(Count));
        }

        public TodoViewModel(TodoItem item)
        {
            SelectedTodo = item;
        }

        public TodoViewModel()
        {
            //base.OnPropertyChanged(nameof(Todos));
            //base.OnPropertyChanged(nameof(Count));

            CreateTodoCommand = new DelegateCommand(p =>
            {
                TodoItem neuesTodo = new TodoItem();
                neuesTodo.Title = $"Todo {Todos.Count + 1}";
                neuesTodo.TimeDue = DateTime.Now.AddSeconds(20);
                Todos.Add(neuesTodo);
                SelectedTodo = neuesTodo;

                NotificationRequested?.Invoke(this, neuesTodo);

            });
        }

        public void UpdateToastNotification()
        {

        }
    }
}
