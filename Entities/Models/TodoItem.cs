using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Entities.Models
{
    public class TodoItem : ModelBase, INotifyPropertyChanged
    {
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

        public NotifyProperty<string> Desc { get; set; } = new NotifyProperty<string>(string.Empty);

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
        public TodoItem(string title, string description, NotifyProperty<string> desc, DateTime timeDue, bool done)
        {
            Title = title;
            Description = description;
            Desc = desc;
            TimeDue = timeDue;
            Done = done;
        }
    }
}
