using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Properties.ValueObjects
{
    public sealed class UnitNumber : SingleValueObject<string>
    {
        public UnitNumber(string value) : base(value)
        {
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new DomainException("Unit number cannot be empty.");
        }
    }
}
