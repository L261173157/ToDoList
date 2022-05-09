using Database.Models.DoList;
using Prism.Events;

namespace DoList.Services.EventType;

public class MainViewNotify : PubSubEvent<Thing>
{
}