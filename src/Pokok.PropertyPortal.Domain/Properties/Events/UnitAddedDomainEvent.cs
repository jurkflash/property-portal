using Pokok.BuildingBlocks.Domain.Events;
using Pokok.PropertyPortal.Domain.Properties.Entities;

namespace Pokok.PropertyPortal.Domain.Properties.Events
{
    public sealed record UnitAddedDomainEvent(PropertyId PropertyId, PropertyUnitId UnitId, string UnitNumber) : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
