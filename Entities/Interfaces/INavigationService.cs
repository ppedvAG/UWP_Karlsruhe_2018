using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
    public interface INavigationService
    {
        void Navigate(NavigationTarget name, ModelBase parameter);
    }

    public enum NavigationTarget { BookSearch, TodoItems, Main }
}
