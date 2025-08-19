using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Properties.ValueObjects
{
    public sealed class PropertyName : SingleValueObject<string>
    {
        public PropertyName(string value) : base(value)
        {
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new DomainException("PropertyName cannot be empty.");
        }
    }
}
