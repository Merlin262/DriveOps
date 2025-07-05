using DriveOps.Application.Commands.CreateVehicle;
using DriveOps.Application.Commands.UpdateVehicleColor;
using DriveOps.Application.Queries.GetAllVehicles;
using DriveOps.Application.Queries.GetVehicleByChassisId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DriveOps.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _mediator.Send(new GetAllVehiclesQuery());
            return Ok(vehicles);
        }

        [HttpGet("{series}/{number}")]
        public async Task<IActionResult> GetByChassisId(string series, uint number)
        {
            var query = new GetVehicleByChassisIdQuery(series, number);
            var vehicle = await _mediator.Send(query);

            if (vehicle == null)
                return NotFound($"Vehicle {series}-{number} not found.");

            return Ok(vehicle);
        }

        [HttpPut("{series}/{number}/color")]
        public async Task<IActionResult> UpdateColor(string series, uint number, [FromBody] string color)
        {
            var command = new UpdateVehicleColorCommand
            {
                ChassisSeries = series,
                ChassisNumber = number,
                Color = color
            };

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound($"Vehicle {series}-{number} not found.");

            return NoContent();
        }
    }
}
