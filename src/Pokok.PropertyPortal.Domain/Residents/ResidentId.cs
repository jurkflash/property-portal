using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Residents
{
    public sealed class ResidentId : EntityId<Guid>
    {
        public ResidentId(Guid value) : base(value) 
        {
            if (value == Guid.Empty)
                throw new DomainException("ResidentId cannot be empty.");
        }
    }
}
