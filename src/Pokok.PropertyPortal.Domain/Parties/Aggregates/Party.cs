using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Enums;
using Pokok.PropertyPortal.Domain.Parties.Events;
using Pokok.PropertyPortal.Domain.Parties.ValueObjects;

namespace Pokok.PropertyPortal.Domain.Parties.Aggregates
{
    public class Party : AggregateRoot<PartyId>
    {
        public SsoStatus SsoStatus { get; private set; } = SsoStatus.Pending;
        public string SsoFailedMessage { get; private set; }
        public Guid? SsoUserId { get; private set; }
        public PartyName PartyName { get; private set; }
        public PartyType PartyType { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }

        private Party() { } // EF

        private Party(PartyName partyName, PartyType partyType, Email email, PhoneNumber? phone = null) : base(PartyId.New())
        {
            PartyName = partyName ?? throw new DomainException("Party name is required.");
            Email = email ?? throw new DomainException("Party email is required.");
            PhoneNumber = phone;
        }

        public static Party Register(PartyName name, PartyType type, Email email, PhoneNumber? phone = null)
        {
            var party = new Party(name, type, email, phone);
            party.AddDomainEvent(new PartyRegistered(name, type, email));
            return party;
        }

        public void ChangePhoneNumber(PhoneNumber? newPhone)
        {
            PhoneNumber = newPhone;
        }

        public void MarkSsoCreated(Guid ssoUserId)
        {
            SsoUserId = ssoUserId;
            SsoStatus = SsoStatus.Created;
        }

        public void MarkSsoFailed(string reason)
        {
            SsoStatus = SsoStatus.Failed;
            SsoFailedMessage = reason;
        }
    }
}
