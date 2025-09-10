using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;

namespace Pokok.PropertyPortal.Application.Queries.GetPropertyById
{
    public sealed record GetPropertyByIdQuery(PropertyId PropertyId) : IQuery<Property>;
}
