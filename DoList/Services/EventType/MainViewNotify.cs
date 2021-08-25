using DoList.Models;
using Prism.Events;

namespace DoList.Services.EventType
{
   public class MainViewNotify: PubSubEvent<Thing>
    {
    }
}
