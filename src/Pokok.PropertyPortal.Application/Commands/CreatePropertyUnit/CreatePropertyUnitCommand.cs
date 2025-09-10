using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;

namespace Pokok.PropertyPortal.Application.Commands.CreatePropertyUnit
{
    public sealed record CreatePropertyUnitCommand(PropertyId PropertyId, UnitNumber UnitNumber) : ICommand<PropertyUnitId>;
}
