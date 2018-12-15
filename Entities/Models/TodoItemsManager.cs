using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Entities.Models
{
    public static class TodoItemsManager
    {
        public static ObservableCollection<TodoItem> TodoItems = new ObservableCollection<TodoItem>();
    }
}
