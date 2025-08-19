using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.PropertyPortal.Domain.Parties;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties.Entities
{
    public class PropertyUnit : Entity<PropertyUnitId>
    {
        private readonly List<Resident> _residents = new();

        public UnitNumber UnitNumber { get; private set; }
        public IReadOnlyCollection<Resident> Residents => _residents.AsReadOnly();

        public PropertyUnit(PropertyUnitId id, UnitNumber unitNumber) : base(id)
        {
            UnitNumber = unitNumber;
        }

        public Resident AssignParty(Party party, ResidentRole residentRole)
        {
            if (_residents.Any(r => r.Party.Email.Value.Equals(party.Email.Value, StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Resident with email '{party.Email}' already exists in unit {UnitNumber.Value}.");

            var resident = new Resident(ResidentId.New(), party, residentRole);
            _residents.Add(resident);
            return resident;
        }

        public void UnassignParty(PartyId partyId)
        {
            var existing = _residents.FirstOrDefault(r => r.Party.Id == partyId)
               ?? throw new DomainException($"Party '{partyId}' not found in unit {UnitNumber.Value}.");
            _residents.Remove(existing);
        }

        public void UpdateResidentRole(PartyId partyId, ResidentRole newRole)
        {
            var resident = _residents.FirstOrDefault(r => r.Party.Id == partyId)
                           ?? throw new DomainException($"Party '{partyId}' not found in unit {UnitNumber.Value}.");
            resident.UpdateRole(newRole);
        }
    }
}
