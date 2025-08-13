using Pokok.BuildingBlocks.Domain.Events;
using Pokok.PropertyPortal.Domain.Properties;

namespace Pokok.PropertyPortal.Domain.Events
{
    public sealed record UnitAddedDomainEvent(PropertyId PropertyId, UnitId UnitId, string UnitNumber) : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
