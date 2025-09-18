using Microsoft.EntityFrameworkCore;
using Pokok.BuildingBlocks.Persistence.Base;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Repositories;
using Pokok.PropertyPortal.Infrastructure.Properties.Persistence;

namespace Pokok.PropertyPortal.Infrastructure.Properties.Repository
{
    public sealed class PartyRepository : RepositoryBase<Party>, IPartyRepository
    {
        private readonly PropertyDbContext _context;

        public PartyRepository(PropertyDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Party?> GetByIdAsync(PartyId id, CancellationToken cancellationToken = default)
        {
            return await _context.Parties.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
