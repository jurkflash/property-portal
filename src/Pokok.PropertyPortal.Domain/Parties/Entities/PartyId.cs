using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Parties.Entities
{
    public sealed class PartyId : EntityId<Guid>
    {
        public PartyId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new DomainException("PartyId cannot be empty.");
        }

        public static PartyId New() => new PartyId(Guid.NewGuid());
    }
}
