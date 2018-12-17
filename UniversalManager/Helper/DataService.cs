using Entities.Interfaces;
using Entities.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace UniversalManager
{
    public class DataService : IDataService
    {

        public const string Local_Settings_Todo = "CurrentTodos";
        public const string Last_Todo_ID = "LastTodoID";

        JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.Objects
        };

       
        public (ObservableCollection<TodoItem> todoitems, int lastId)? LoadData()
        {
            //Todos laden
            if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Local_Settings_Todo, out object json))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.TypeNameHandling = TypeNameHandling.Objects;
                var items = JsonConvert.DeserializeObject<ObservableCollection<TodoItem>>(json.ToString(), settings);

                if (ApplicationData.Current.LocalSettings.Values.TryGetValue(Last_Todo_ID, out object lastID))
                {
                    return (items, (int)lastID);
                }
            }
            return null;
        }

        public void SaveData(ObservableCollection<TodoItem> todoitems, int lastID)
        {
            //TodoItems speichern
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            string json = JsonConvert.SerializeObject(todoitems, _settings);

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(Local_Settings_Todo))
            {
                ApplicationData.Current.LocalSettings.Values[Local_Settings_Todo] = json;
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values.Add(Local_Settings_Todo, json);
                ApplicationData.Current.LocalSettings.Values.Add(Last_Todo_ID, lastID);
            }
        }
    }
}
