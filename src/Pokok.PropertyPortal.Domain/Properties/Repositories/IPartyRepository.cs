using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Entities;

namespace Pokok.PropertyPortal.Domain.Parties.Repositories
{
    public interface IPartyRepository : IRepository<Party>
    {
        Task<Party?> GetByIdAsync(PartyId id, CancellationToken cancellationToken = default);
    }
}
