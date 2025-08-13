using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.PropertyPortal.Domain.Common;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties
{
    public class Property : AggregateRoot<PropertyId>
    {
        public string Name { get; private set; }
        public string Address { get; private set; }

        private readonly List<Unit> _units = new();
        public IReadOnlyCollection<Unit> Units => _units.AsReadOnly();

        public Property(PropertyId id, string name, string address) : base(id)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Property name cannot be empty.");

            Name = name;
            Address = address ?? throw new DomainException("Property address is required.");
        }

        public static Property Create(PropertyId id, string name, string address) => new(id, name, address);

        public void AddUnit(Unit unit)
        {
            if (_units.Any(u => u.Number == unit.Number))
                throw new DomainException($"Unit {unit.Number} already exists in this property.");

            AddDomainEvent(new UnitAddedDomainEvent(Id, unit.Id, unit.Number));
            _units.Add(unit);
        }

        public void RemoveUnit(UnitId unitId)
        {
            var unit = _units.FirstOrDefault(u => u.Id == unitId)
                       ?? throw new DomainException($"Unit '{unitId}' not found.");
            _units.Remove(unit);
            // Optional: raise UnitRemoved event
        }

        public void RegisterResident(UnitId unitId, Resident resident)
        {
            var unit = _units.FirstOrDefault(u => u.Id == unitId)
                       ?? throw new DomainException($"Unit '{unitId}' not found.");

            unit.AddResident(resident);
            AddDomainEvent(new ResidentRegisteredDomainEvent(Id, unitId, resident.Id));
        }

        public Unit GetUnitByNumber(string unitNumber)
        {
            return _units.FirstOrDefault(u => u.Number.Equals(unitNumber, StringComparison.OrdinalIgnoreCase))
                   ?? throw new DomainException($"Unit '{unitNumber}' not found.");
        }
    }
}
