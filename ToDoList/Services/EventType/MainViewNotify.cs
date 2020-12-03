using System;
using System.Collections.Generic;
using System.Text;
using Prism.Events;
using ToDoList.Models;
namespace ToDoList.Services.EventType
{
   public class MainViewNotify: PubSubEvent<Thing>
    {
    }
}
