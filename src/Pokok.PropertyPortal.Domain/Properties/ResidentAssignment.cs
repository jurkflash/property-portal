using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Residents;

namespace Pokok.PropertyPortal.Domain.Properties
{
    public class ResidentAssignment : Entity<Guid> // could also be ValueObject
    {
        public PropertyUnitId UnitId { get; private set; }
        public ResidentRole Role { get; private set; }

        private ResidentAssignment() { } // EF

        public ResidentAssignment(PropertyUnitId unitId, ResidentRole role)
        {
            UnitId = unitId ?? throw new DomainException("UnitId required.");
            Role = role;
        }
    }
}
