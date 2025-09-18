using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Entities;

namespace Pokok.PropertyPortal.Application.Queries.GetPartyById
{
    public sealed record GetPartyByIdQuery(PartyId PartyId) : IQuery<Party>;
}
