using DriveOps.Services.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.UpdateVehicleColor
{
    public class UpdateVehicleColorCommandHandler : IRequestHandler<UpdateVehicleColorCommand, bool>
    {
        private readonly IVehicleService _vehicleService;

        public UpdateVehicleColorCommandHandler(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<bool> Handle(UpdateVehicleColorCommand request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleService.GetVehicleByChassisIdAsync(request.ChassisSeries, request.ChassisNumber, cancellationToken);
            if (vehicle == null)
                return false;

            return await _vehicleService.UpdateVehicleColorAsync(vehicle, request.Color, cancellationToken);
        }
    }
}
