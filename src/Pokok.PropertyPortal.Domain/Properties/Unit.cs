using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.PropertyPortal.Domain.Common;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties
{
    public class Unit : Entity<UnitId>
    {
        private readonly List<Resident> _residents = new();

        public string Number { get; private set; }
        public IReadOnlyCollection<Resident> Residents => _residents.AsReadOnly();

        public Unit(UnitId id, string number) : base(id)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new DomainException("Unit number cannot be empty.");

            Number = number;
        }

        internal void AddResident(Resident resident)
        {
            if (_residents.Any(r => r.Email.Value.Equals(resident.Email.Value, StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Resident with email '{resident.Email}' already exists in unit {Number}.");

            _residents.Add(resident);
        }

        public void RemoveResident(ResidentId residentId)
        {
            var existing = _residents.FirstOrDefault(r => r.Id == residentId)
                           ?? throw new DomainException($"Resident '{residentId}' not found in unit {Number}.");
            _residents.Remove(existing);
        }
    }
}
