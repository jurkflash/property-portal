using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Application.Commands.CreatePropertyUnit
{
    public sealed class CreatePropertyUnitCommandHandler : ICommandHandler<CreatePropertyUnitCommand, PropertyUnitId>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePropertyUnitCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PropertyUnitId> HandleAsync(CreatePropertyUnitCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);

            var unit = PropertyUnit.Create(command.UnitNumber);
            property.AddUnit(unit);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return unit.Id;
        }
    }
}
