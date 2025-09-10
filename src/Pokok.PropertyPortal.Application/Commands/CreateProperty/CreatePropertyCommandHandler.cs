using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Application.Commands.CreateProperty
{
    public sealed class CreatePropertyCommandHandler : ICommandHandler<CreatePropertyCommand, PropertyId>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyId> HandleAsync(CreatePropertyCommand command, CancellationToken cancellationToken)
        {
            var property = Property.Create(
                command.PropertyName,
                command.Address
            );

            await _propertyRepository.AddAsync(property, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return property.Id;
        }
    }
}
