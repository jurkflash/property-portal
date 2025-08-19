using Microsoft.AspNetCore.Mvc;
using Pokok.BuildingBlocks.Cqrs.Dispatching;
using Pokok.PropertyPortal.Application.Commands;

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
        public async Task<IActionResult> CreateProperty(CreatePropertyCommand createPropertyCommand)
        {
            var propertyId = await _commandDispatcher.DispatchAsync<CreatePropertyCommand, Guid>(createPropertyCommand);
            return Ok(propertyId);
            //return CreatedAtAction(nameof(GetProperty), new { id }, null);
        }
    }
}
