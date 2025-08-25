using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Enums;
using Pokok.PropertyPortal.Domain.Parties.ValueObjects;

namespace Pokok.PropertyPortal.Domain.Parties.Aggregates
{
    public class Party : AggregateRoot<PartyId>
    {
        public PartyName PartyName { get; private set; }
        public PartyType PartyType { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? Phone { get; private set; }

        private Party() { } // EF

        private Party(PartyId id, PartyName partyName, PartyType partyType, Email email, PhoneNumber? phone = null)
            : base(id)
        {
            PartyName = partyName ?? throw new DomainException("Party name is required.");
            Email = email ?? throw new DomainException("Party email is required.");
            Phone = phone;
        }

        public static Party Register(PartyId id, PartyName name, PartyType type, Email email, PhoneNumber? phone = null)
        {
            var party = new Party(id, name, type, email, phone);
            party.AddDomainEvent(new PartyRegistered(name, email, type));
            return party;
        }

        public void ChangePhoneNumber(PhoneNumber? newPhone)
        {
            Phone = newPhone;
        }
    }
}
