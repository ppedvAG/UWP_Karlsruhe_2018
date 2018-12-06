using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    /// <summary>
    /// Basis-Klasse aller Notify Klassen
    /// </summary>
    public class NotifyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        /// <summary>
        /// Vereinfacht das Aufrufen von PropertyChanged, da es über den CallerMemberName den Namen der Variable an den hörenden weiterreicht.
        /// </summary>
        /// <returns>Wird der Wert geändert, dann wird true zurückgegeben (erfolgt nur wenn der neue Wert sich vom alten unterscheidet)</returns>
        protected bool SetPropertyValue<T>(ref T variable, T value,
        [CallerMemberName] String propertyName = null)
        {
            if (Equals(variable, value)) return false;
            variable = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
