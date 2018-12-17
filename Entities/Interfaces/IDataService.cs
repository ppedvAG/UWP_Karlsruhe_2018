using Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Entities.Interfaces
{
    public interface IDataService
    {
        void SaveData(ObservableCollection<TodoItem> todoitems, int lastID);
        (ObservableCollection<TodoItem> todoitems, int lastId)? LoadData();
    }
}
