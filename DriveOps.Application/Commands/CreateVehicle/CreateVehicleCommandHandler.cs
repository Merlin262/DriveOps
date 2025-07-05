using DriveOps.Domain.ValueObjects;
using DriveOps.Services.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleResult>
    {
        private readonly IVehicleService _vehicleService;

        public CreateVehicleCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<CreateVehicleResult> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var chassisId = await _vehicleService.CreateVehicleAsync(
                request.Type,
                request.ChassisSeries,
                request.ChassisNumber,
                request.Color,
                cancellationToken
            );

            return new CreateVehicleResult
            {
                ChassisId = chassisId
            };
        }
    }
}