using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;

namespace Pokok.PropertyPortal.Application.Commands
{
    public sealed record CreatePropertyCommand(PropertyName PropertyName, Address Address) : ICommand<Guid>;
}
