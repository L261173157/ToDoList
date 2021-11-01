using Database.Models.DoList;

using Prism.Events;

namespace DoList.Services.EventType
{
    class EditViewTransmit: PubSubEvent<Thing>
    {
    }
}
