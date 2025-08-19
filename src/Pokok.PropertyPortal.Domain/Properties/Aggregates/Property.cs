using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Events;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties.Aggregates
{
    public class Property : AggregateRoot<PropertyId>
    {
        public PropertyName PropertyName { get; private set; }
        public Address Address { get; private set; }

        private readonly List<PropertyUnit> _units = new();
        public IReadOnlyCollection<PropertyUnit> Units => _units.AsReadOnly();

        private Property(PropertyName propertyName, Address address) : base(PropertyId.New())
        {
            PropertyName = propertyName ?? throw new DomainException("Property name is required.");
            Address = address ?? throw new DomainException("Property address is required.");
        }

        public static Property Create(PropertyName propertyName, Address address)
        {
            var property = new Property(propertyName, address);

            // if you want an event for creation
            // property.AddDomainEvent(new PropertyCreatedDomainEvent(property.Id, property.PropertyName.Value));

            return property;
        }

        public void AddUnit(PropertyUnit propertyUnit)
        {
            if (_units.Any(u => u.UnitNumber.Equals(propertyUnit.UnitNumber)))
                throw new DomainException($"Unit {propertyUnit.UnitNumber.Value} already exists in this property.");

            _units.Add(propertyUnit);
            AddDomainEvent(new UnitAddedDomainEvent(Id, propertyUnit.Id, propertyUnit.UnitNumber.Value));
        }

        public void RemoveUnit(PropertyUnitId unitId)
        {
            var unit = _units.FirstOrDefault(u => u.Id == unitId)
                       ?? throw new DomainException($"Unit '{unitId}' not found.");
            _units.Remove(unit);
            // Optional: raise UnitRemoved event
        }

        public void RegisterResident(PropertyUnitId unitId, Resident resident)
        {
            var unit = _units.FirstOrDefault(u => u.Id == unitId)
                       ?? throw new DomainException($"Unit '{unitId}' not found.");

            unit.AddResident(resident);
            AddDomainEvent(new ResidentRegisteredDomainEvent(Id, unitId, resident.Id));
        }

        public PropertyUnit GetUnitByNumber(string unitNumber)
        {
            return _units.FirstOrDefault(u => u.UnitNumber.Value.Equals(unitNumber, StringComparison.OrdinalIgnoreCase))
                   ?? throw new DomainException($"Unit '{unitNumber}' not found.");
        }
    }
}
