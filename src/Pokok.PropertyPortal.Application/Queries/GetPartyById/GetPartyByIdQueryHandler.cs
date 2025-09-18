using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Application.Queries.GetPartyById;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Repositories;

namespace Pokok.PropertyPortal.Application.Queries.GetPropertyById
{
    public sealed class GetPartyByIdQueryHandler : IQueryHandler<GetPartyByIdQuery, Party>
    {
        private readonly IPartyRepository _partyRepository;

        public GetPartyByIdQueryHandler(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public async Task<Party> HandleAsync(GetPartyByIdQuery query, CancellationToken cancellationToken)
        {
            var party = await _partyRepository.GetByIdAsync(query.PartyId, cancellationToken);

            if (party is null)
            {
                throw new InvalidOperationException($"Party with ID {query.PartyId} was not found.");
            }

            return party;
        }
    }
}
