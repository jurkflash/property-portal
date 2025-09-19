using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Entities;

namespace Pokok.PropertyPortal.Domain.Residents
{
    public class Resident : Entity<ResidentId>
    {
        public PartyId PartyId { get; private set; }
        public PhoneNumber? PhoneNumber { get; private set; }
        public ResidentRole ResidentRole { get; private set; }

        private Resident() { } // For EF Core

        public Resident(ResidentId id, PartyId partyId, ResidentRole role, PhoneNumber? phone = null)
        : base(id)
        {
            PartyId = partyId ?? throw new DomainException("Party is required.");
            ResidentRole = role;
            PhoneNumber = phone;
        }
        public void UpdateRole(ResidentRole newRole)
        {
            ResidentRole = newRole;
        }
    }
}
