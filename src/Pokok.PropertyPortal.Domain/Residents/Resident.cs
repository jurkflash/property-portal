using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Common;
using Pokok.PropertyPortal.Domain.Enums;

namespace Pokok.PropertyPortal.Domain.Residents
{
    public class Resident : Entity<ResidentId>
    {
        public PersonName Name { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? Phone { get; private set; }
        public ResidentRole Role { get; private set; }

        public Resident(ResidentId id, PersonName name, Email email, ResidentRole role, PhoneNumber? phone = null)
        : base(id)
        {
            Name = name ?? throw new DomainException("Resident name is required.");
            Email = email ?? throw new DomainException("Resident email is required.");
            Role = role;
            Phone = phone; // optional
        }
    }
}
