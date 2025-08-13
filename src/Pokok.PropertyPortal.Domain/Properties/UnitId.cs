using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Properties
{
    public sealed class UnitId : EntityId<Guid>
    {
        public UnitId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new DomainException("UnitId cannot be empty.");
        }
    }
}
