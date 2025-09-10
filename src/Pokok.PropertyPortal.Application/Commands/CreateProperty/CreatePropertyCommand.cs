using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;

namespace Pokok.PropertyPortal.Application.Commands.CreateProperty
{
    public sealed record CreatePropertyCommand(PropertyName PropertyName, Address Address) : ICommand<PropertyId>;
}
