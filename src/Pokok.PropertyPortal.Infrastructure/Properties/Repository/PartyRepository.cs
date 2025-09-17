using Pokok.BuildingBlocks.Persistence.Base;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
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
    }
}
