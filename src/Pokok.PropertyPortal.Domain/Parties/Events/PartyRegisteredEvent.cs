using Pokok.BuildingBlocks.Domain.Events;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Enums;
using Pokok.PropertyPortal.Domain.Parties.ValueObjects;

namespace Pokok.PropertyPortal.Domain.Parties.Events
{
    public sealed record PartyRegisteredEvent(PartyId PartyId, PartyName name, PartyType type, Email email) : IDomainEvent
    {
        public DateTime OccurredOn => DateTime.UtcNow;
    }
}
