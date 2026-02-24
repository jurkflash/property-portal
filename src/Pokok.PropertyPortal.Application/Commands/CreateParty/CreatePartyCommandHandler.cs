using Pokok.BuildingBlocks.Cqrs.Abstractions;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Application.Commands.CreateParty;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
using Pokok.PropertyPortal.Domain.Parties.Entities;
using Pokok.PropertyPortal.Domain.Parties.Repositories;

namespace Pokok.PropertyPortal.Application.Commands.CreateProperty
{
    public sealed class CreatePartyCommandHandler : ICommandHandler<CreatePartyCommand, PartyId>
    {
        private readonly IPartyRepository _partyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePartyCommandHandler(IPartyRepository propertyRepository, IUnitOfWork unitOfWork)
        {
            _partyRepository = propertyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PartyId> HandleAsync(CreatePartyCommand command, CancellationToken cancellationToken)
        {
            var party = Party.Register(
                command.PartyName,
                command.PartyType,
                command.Email, 
                command.Phone
            );

            await _partyRepository.AddAsync(party, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            return party.Id;
        }
    }
}
