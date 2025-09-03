using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Application.Commands
{
    public sealed class CreatePropertyCommandHandler : ICommandHandler<CreatePropertyCommand, Guid>
    {
        private readonly IPropertyRepository _propertyRepository;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Guid> HandleAsync(CreatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = Property.Create(
                command.PropertyName,
                command.Address
            );

            await _propertyRepository.AddAsync(property, cancellationToken);
            return property.Id.Value;
        }
    }
}
