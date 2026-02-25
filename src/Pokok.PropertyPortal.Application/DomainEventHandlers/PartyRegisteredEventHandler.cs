using Microsoft.Extensions.Logging;
using Pokok.BuildingBlocks.Domain.Events;
using Pokok.BuildingBlocks.Persistence.Abstractions;
using Pokok.PropertyPortal.Application.Services;
using Pokok.PropertyPortal.Domain.Parties.Events;
using Pokok.PropertyPortal.Domain.Parties.Repositories;
using System;
using System.Threading.Tasks;

namespace Pokok.PropertyPortal.Application.DomainEventHandlers
{
    public class PartyRegisteredEventHandler : IDomainEventHandler<PartyRegisteredEvent>
    {
        private readonly IIdentityService _identityService;
        private readonly IPartyRepository _partyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PartyRegisteredEventHandler> _logger;

        public PartyRegisteredEventHandler(
            IIdentityService identityService,
            IPartyRepository partyRepository,
            IUnitOfWork unitOfWork,
            ILogger<PartyRegisteredEventHandler> logger)
        {
            _identityService = identityService;
            _partyRepository = partyRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(PartyRegisteredEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling PartyRegisteredEvent for Party {PartyId}, provisioning SSO user with email {Email}",
                domainEvent.PartyId.Value, domainEvent.email.Value);

            var party = await _partyRepository.GetByIdAsync(domainEvent.PartyId, cancellationToken)
                ?? throw new InvalidOperationException($"Party {domainEvent.PartyId.Value} not found.");

            try
            {
                var ssoUserId = await _identityService.CreateUserAsync(
                    domainEvent.email.Value,
                    domainEvent.name.Value,
                    cancellationToken);

                party.MarkSsoCreated(ssoUserId);

                _logger.LogInformation("SSO user {SsoUserId} created for Party {PartyId}",
                    ssoUserId, domainEvent.PartyId.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create SSO user for Party {PartyId}", domainEvent.PartyId.Value);
                party.MarkSsoFailed(ex.Message);
            }

            await _unitOfWork.CompleteAsync(cancellationToken);
        }
    }
}
