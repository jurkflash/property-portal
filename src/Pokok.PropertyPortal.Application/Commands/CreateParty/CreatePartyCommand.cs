using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Enums;
using Pokok.PropertyPortal.Domain.Parties.ValueObjects;

namespace Pokok.PropertyPortal.Application.Commands.CreateParty
{
    public sealed record CreatePartyCommand(PartyName PartyName, PartyType PartyType, Email Email, PhoneNumber? Phone) : ICommand<PartyId>;
}
