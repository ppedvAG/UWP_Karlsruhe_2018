using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class NotifyProperty<T> : NotifyBase
    {
        
        private T _Value ;
        public T Value
        {
            get
            {
                return _Value;
            }
            set
            {
                SetPropertyValue(ref _Value, value);
            }
        }

        public NotifyProperty(T value)
        {
            Value =  value;
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(Value));
        }
    }
}
