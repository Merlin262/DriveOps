using DriveOps.Services.Services;
using FluentValidation;
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
        private readonly IValidator<UpdateVehicleColorCommand> _validator;

        public UpdateVehicleColorCommandHandler(
            IVehicleService vehicleService,
            IValidator<UpdateVehicleColorCommand> validator)
        {
            _vehicleService = vehicleService;
            _validator = validator;
        }

        public async Task<bool> Handle(UpdateVehicleColorCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var vehicle = await _vehicleService.GetVehicleByChassisIdAsync(request.ChassisSeries, request.ChassisNumber, cancellationToken);
            if (vehicle == null)
                return false;

            return await _vehicleService.UpdateVehicleColorAsync(vehicle, request.Color, cancellationToken);
        }
    }
}
