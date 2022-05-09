using Database.Models.DoList;
using Prism.Events;

namespace DoList.Services.EventType;

internal class EditViewTransmit : PubSubEvent<Thing>
{
}