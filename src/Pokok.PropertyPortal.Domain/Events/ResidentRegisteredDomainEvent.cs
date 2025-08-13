using Pokok.BuildingBlocks.Domain.Events;
using Pokok.PropertyPortal.Domain.Properties;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Events
{
    public sealed record ResidentRegisteredDomainEvent(PropertyId PropertyId, UnitId UnitId, ResidentId ResidentId) : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
