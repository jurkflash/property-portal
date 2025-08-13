using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Domain.Properties;

namespace Pokok.PropertyPortal.Domain.Repositories
{
    public interface IPropertyRepository : IRepository<Property>
    {
        // Strongly-typed ID convenience (infra can route to Guid-based method internally)
        Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default);

        // Domain-specific queries
        Task<Property?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<Property?> GetByUnitNumberAsync(PropertyId propertyId, string unitNumber, CancellationToken cancellationToken = default);
    }
}
