using Microsoft.EntityFrameworkCore;
using Pokok.BuildingBlocks.Persistence.Base;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.Repositories;
using Pokok.PropertyPortal.Infrastructure.Properties.Persistence;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Repository
{
    public sealed class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
    {
        private readonly PropertyDbContext _context;

        public PropertyRepository(PropertyDbContext context) : base(context)
        {
            _context = context;
        }

        //public override Task AddAsync(Property property, CancellationToken cancellationToken = default)
        //{
        //    _properties.Add(property);
        //    return Task.CompletedTask;
        //}

        public async Task<Property?> GetByIdAsync(PropertyId id, CancellationToken cancellationToken = default)
        {
            return await _context.Properties.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
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
