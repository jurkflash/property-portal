using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Application.Commands.RemovePropertyUnit;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Application.Commands.CreatePropertyUnit
{
    public sealed class RemovePropertyUnitCommandHandler : ICommandHandler<RemovePropertyUnitCommand, bool>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePropertyUnitCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _propertyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> HandleAsync(RemovePropertyUnitCommand command, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.SingleOrDefaultAsync(p => p.Id == command.PropertyId, cancellationToken);

            property.RemoveUnit(command.PropertyUnitId);

            await _unitOfWork.CompleteAsync(cancellationToken);

            return true;
        }
    }
}
