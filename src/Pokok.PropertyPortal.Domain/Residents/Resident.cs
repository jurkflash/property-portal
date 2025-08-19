using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties;

namespace Pokok.PropertyPortal.Domain.Residents
{
    public class Resident : Entity<ResidentId>
    {
        public Party Party { get; private set; }
        public PhoneNumber? Phone { get; private set; }
        public ResidentRole Role { get; private set; }

        private Resident() { } // For EF Core

        public Resident(ResidentId id, Party party, ResidentRole role, PhoneNumber? phone = null)
        : base(id)
        {
            Party = party ?? throw new DomainException("Party is required.");
            Role = role;
            Phone = phone;
        }
    }
}
