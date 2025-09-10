using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.PropertyPortal.Api.Requests;
using Pokok.PropertyPortal.Application.Commands;
using Pokok.PropertyPortal.Application.Commands.CreateProperty;
using Pokok.PropertyPortal.Application.Commands.CreatePropertyUnit;
using Pokok.PropertyPortal.Application.Queries.GetPropertyById;
using Pokok.PropertyPortal.Domain.Properties.Aggregates;
using Pokok.PropertyPortal.Domain.Properties.Entities;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using SharedKernelAddress = Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects.Address;

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
            var propertyId = await _commandDispatcher.DispatchAsync<CreatePropertyCommand, PropertyId>(command);
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

        [HttpPost("{id:guid}/units")]
        public async Task<IActionResult> CreatePropertyUnit(Guid id, [FromBody] CreatePropertyUnitRequest request)
        {
            var command = new CreatePropertyUnitCommand(
                new PropertyId(id),
                new UnitNumber(request.UnitNumber)
            );
            var propertyUnitId = await _commandDispatcher.DispatchAsync<CreatePropertyUnitCommand, PropertyUnitId>(command);

            return Ok(propertyUnitId.Value);
        }
    }
}
