using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Entities;

namespace Pokok.PropertyPortal.Application.Commands.RemovePropertyUnit
{
    public sealed record RemovePropertyUnitCommand(PropertyId PropertyId, PropertyUnitId PropertyUnitId) : ICommand<bool>;
}
