using DoList.Models;
using Prism.Events;

namespace DoList.Services.EventType
{
    class EditViewTransmit: PubSubEvent<Thing>
    {
    }
}
