using Microsoft.AspNetCore.Mvc;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects;
using Pokok.PropertyPortal.Api.Requests;
using Pokok.PropertyPortal.Application.Commands.CreateParty;
using Pokok.PropertyPortal.Application.Queries.GetPartyById;
using Pokok.PropertyPortal.Domain.Parties.Aggregates;
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
                new Email(request.Email),
                new PhoneNumber(request.PhoneNumber)
                );
            var partyId = await _commandDispatcher.DispatchAsync<CreatePartyCommand, PartyId>(command);
            return Ok(partyId);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetParty(Guid id)
        {
            var partyId = new PartyId(id); // Convert Guid to PartyId
            var query = new GetPartyByIdQuery(partyId);
            var result = await _queryDispatcher.DispatchAsync<GetPartyByIdQuery, Party>(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
