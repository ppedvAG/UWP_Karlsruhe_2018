using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities.Models
{
    public class TodoItem : ModelBase, INotifyPropertyChanged
    {
        public static int Last_TODO_ID = 0;

        private int _id;
        public int ID
        {
            get { return _id; }
            set { SetValue(ref _id, value); }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set {
                SetValue(ref _title, value);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetValue(ref _description, value); }
        }

        // public NotifyProperty<string> Desc { get; set; } = new NotifyProperty<string>(string.Empty);

        private DateTime _timeDue;
        public DateTime TimeDue
        {
            get { return _timeDue; }
            set {
                SetValue(ref _timeDue, value);
            }
        }

        private bool _done;
        public bool Done
        {
            get { return _done; }
            set { SetValue(ref _done, value); }
        }

        public TodoItem()
        {

        }

        [JsonConstructor]
        public TodoItem(int id, string title, string description, DateTime? timeDue = null, bool done = false)
        {
            Title = title;
            Description = description;
            TimeDue = timeDue == null ? DateTime.Now.AddDays(7) : timeDue.Value;
            Done = done;
            ID = id;
        }
    }
}
