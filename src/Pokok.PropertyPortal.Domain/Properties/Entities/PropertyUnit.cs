using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties.Entities
{
    public class PropertyUnit : Entity<PropertyUnitId>
    {
        private readonly List<Resident> _residents = new();

        public UnitNumber UnitNumber { get; private set; }
        public IReadOnlyCollection<Resident> Residents => _residents.AsReadOnly();

        private PropertyUnit() { } // For EF Core

        public PropertyUnit(PropertyUnitId id, UnitNumber unitNumber) : base(id)
        {
            UnitNumber = unitNumber;
        }

        public static PropertyUnit Create(UnitNumber unitNumber)
        {
            if (unitNumber is null) throw new ArgumentNullException(nameof(unitNumber));
            return new PropertyUnit(PropertyUnitId.New(), unitNumber);
        }

        public Resident AssignParty(PartyId partyId, ResidentRole residentRole)
        {
            if (_residents.Any(r => r.PartyId.Value == partyId.Value))
                throw new DomainException($"Resident already exists in unit {UnitNumber.Value}.");

            var resident = new Resident(ResidentId.New(), partyId, residentRole);
            _residents.Add(resident);
            return resident;
        }

        public void UnassignParty(PartyId partyId)
        {
            var existing = _residents.FirstOrDefault(r => r.PartyId.Value == partyId.Value)
               ?? throw new DomainException($"Party not found in unit {UnitNumber.Value}.");
            _residents.Remove(existing);
        }

        public void UpdateResidentRole(PartyId partyId, ResidentRole newRole)
        {
            var resident = _residents.FirstOrDefault(r => r.PartyId.Value == partyId.Value)
                           ?? throw new DomainException($"Party not found in unit {UnitNumber.Value}.");
            resident.UpdateRole(newRole);
        }
    }
}
