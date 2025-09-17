using Microsoft.AspNetCore.Mvc;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.PropertyPortal.Api.Requests;
using Pokok.PropertyPortal.Application.Commands.CreateParty;
using Pokok.PropertyPortal.Domain.Parties.Entities;

namespace Pokok.PropertyPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartiesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public PartiesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateParty(CreatePartyRequest request)
        {
            var command = new CreatePartyCommand(
                new Domain.Parties.ValueObjects.PartyName(request.Name),
                request.PartyType,
                new BuildingBlocks.Domain.SharedKernel.ValueObjects.Email(request.Email),
                new BuildingBlocks.Domain.SharedKernel.ValueObjects.PhoneNumber(request.PhoneNumber)
                );
            var partyId = await _commandDispatcher.DispatchAsync<CreatePartyCommand, PartyId>(command);
            return Ok(partyId);
        }

    }
}
