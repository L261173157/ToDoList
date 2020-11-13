using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;

namespace ToDoList.Services
{
   public interface IEventAggregator
    {
        TEventType GetEvent<TEventType>() where TEventType : EventBase;
    }
}
