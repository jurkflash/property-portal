using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;

namespace Pokok.PropertyPortal.Domain.Parties
{
    public class Party : Entity<PartyId>
    {
        public PartyName PartyName { get; private set; }
        public PartyType PartyType { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? Phone { get; private set; }

        private Party() { } // EF

        public Party(PartyId id, PartyName partyName, PartyType partyType, Email email, PhoneNumber? phone = null) : base(id)
        {
            PartyName = partyName ?? throw new DomainException("Party name is required.");
            Email = email ?? throw new DomainException("Party email is required.");
            PartyType = partyType;
            Phone = phone;
        }
    }
}
