using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Properties.Entities
{
    public sealed class PropertyUnitId : EntityId<Guid>
    {
        public PropertyUnitId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new DomainException("PropertyUnitId cannot be empty.");
        }

        public static PropertyUnitId New() => new PropertyUnitId(Guid.NewGuid());
    }
}
