using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Properties.Entities
{
    public sealed class PropertyId : EntityId<Guid>
    {
        public PropertyId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new DomainException("PropertyId cannot be empty.");
        }

        public static PropertyId New() => new PropertyId(Guid.NewGuid());
    }
}
