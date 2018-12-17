using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(TodoItem item);
    }
}
