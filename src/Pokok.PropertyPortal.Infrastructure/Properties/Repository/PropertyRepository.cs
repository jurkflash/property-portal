using Microsoft.EntityFrameworkCore;
using Pokok.BuildingBlocks.Persistence.Base;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.Repositories;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Repository
{
    public sealed class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        private readonly List<Property> _properties = new();

        public PropertyRepository(DbContext context) : base(context)
        {
        }

        public override Task AddAsync(Property property, CancellationToken cancellationToken = default)
        {
            _properties.Add(property);
            return Task.CompletedTask;
        }

        public Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_properties.FirstOrDefault(p => p.Id == id));
        }

        public Task<Property?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Property?> GetByUnitNumberAsync(PropertyId propertyId, string unitNumber, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
