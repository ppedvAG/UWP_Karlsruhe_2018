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
            CreateNewTodo($"{book.Title} lesen!", book.Description, DateTime.Now + TimeSpan.FromDays(10));
        }

        public TodoViewModel(TodoItem item)
        {
            SelectedTodo = item;
        }

        public void CreateNewTodo(string title, string description, DateTime timeDue)
        {
            TodoItem.Last_TODO_ID++;
            TodoItem neuesTodo = new TodoItem(TodoItem.Last_TODO_ID, title, description, timeDue);
            Todos.Add(neuesTodo);
            base.OnPropertyChanged(nameof(Count));
            SelectedTodo = neuesTodo;

            NotificationRequested?.Invoke(this, neuesTodo);
        }

        public TodoViewModel()
        {
            CreateTodoCommand = new DelegateCommand(p =>
            {
                CreateNewTodo($"Todo {Todos.Count + 1}", "...", DateTime.Now.AddSeconds(20));
            });
        }

        public void UpdateToastNotification()
        {

        }
    }
}