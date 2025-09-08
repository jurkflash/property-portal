using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Application.Queries
{
    public sealed class GetPropertyByIdQueryHandler : IQueryHandler<GetPropertyByIdQuery, Property>
    {
        private readonly IPropertyRepository _propertyRepository;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        public async Task<Property> HandleAsync(GetPropertyByIdQuery query, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(query.PropertyId, cancellationToken);
            //var property = await _propertyRepository.GetAsync(query.PropertyId.Value, cancellationToken);

            if (property is null)
            {
                throw new InvalidOperationException($"Property with ID {query.PropertyId} was not found.");
            }

            return property;
        }
    }
}
