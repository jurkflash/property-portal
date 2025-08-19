using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Properties;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Parties
{
    public class Party : Entity<PartyId>
    {
        public PartyName PartyName { get; private set; }
        public PartyType PartyType { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber? Phone { get; private set; }

        private readonly List<ResidentAssignment> _assignments = new();
        public IReadOnlyCollection<ResidentAssignment> Assignments => _assignments.AsReadOnly();

        private Party() { } // EF

        public Party(PartyId id, PartyName partyName, PartyType partyType, Email email, PhoneNumber? phone = null) : base(id)
        {
            PartyName = partyName ?? throw new DomainException("Party name is required.");
            Email = email ?? throw new DomainException("Party email is required.");
            PartyType = partyType;
            Phone = phone;
        }

        public void AssignToUnit(PropertyUnit unit, ResidentRole role)
        {
            if (_assignments.Any(a => a.UnitId == unit.Id))
                throw new DomainException($"Party already assigned to unit {unit.UnitNumber.Value}.");

            var assignment = new ResidentAssignment(unit.Id, role);
            _assignments.Add(assignment);
        }

        public void RemoveAssignment(PropertyUnitId unitId)
        {
            var existing = _assignments.FirstOrDefault(a => a.UnitId == unitId)
                           ?? throw new DomainException($"No assignment found for unit {unitId}.");
            _assignments.Remove(existing);
        }
    }
}
