using Microsoft.AspNetCore.Mvc;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.PropertyPortal.Application.Commands;
using Pokok.PropertyPortal.Api.Requests;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using SharedKernelAddress = Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects.Address;
using Pokok.PropertyPortal.Application.Queries;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;

namespace Pokok.PropertyPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public PropertiesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProperty(CreatePropertyRequest request)
        {
            var command = new CreatePropertyCommand(
                new PropertyName(request.PropertyName),
                new SharedKernelAddress(
                    request.Street,
                    request.City,
                    request.State,
                    request.PostalCode,
                    request.Country
                )
            );
            var propertyId = await _commandDispatcher.DispatchAsync<CreatePropertyCommand, Guid>(command);
            return Ok(propertyId);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetProperty(Guid id)
        {
            var propertyId = new PropertyId(id); // Convert Guid to PropertyId
            var query = new GetPropertyByIdQuery(propertyId);
            var result = await _queryDispatcher.DispatchAsync<GetPropertyByIdQuery, Property>(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
