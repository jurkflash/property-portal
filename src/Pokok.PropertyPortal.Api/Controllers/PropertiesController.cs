using Microsoft.AspNetCore.Mvc;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.PropertyPortal.Application.Commands;
using Pokok.PropertyPortal.Api.Requests;
using Pokok.PropertyPortal.Domain.Properties.ValueObjects;
using SharedKernelAddress = Pokok.BuildingBlocks.Domain.SharedKernel.ValueObjects.Address;

namespace Pokok.PropertyPortal.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public PropertiesController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
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
    }
}
