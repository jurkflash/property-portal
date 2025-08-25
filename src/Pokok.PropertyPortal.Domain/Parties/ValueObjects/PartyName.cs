using Pokok.BuildingBlocks.Domain.Abstractions;
using Pokok.BuildingBlocks.Domain.Exceptions;

namespace Pokok.PropertyPortal.Domain.Parties.ValueObjects
{
    public sealed class PartyName : SingleValueObject<string>
    {
        public PartyName(string value) : base(value)
        {
        }

        protected override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Value))
                throw new DomainException("PartyName cannot be empty.");
        }
    }
}
