using Pokok.BuildingBlocks.Domain.Events;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Events
{
    public sealed record ResidentRegistered(PropertyId PropertyId, PropertyUnitId UnitId, ResidentId ResidentId) : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
