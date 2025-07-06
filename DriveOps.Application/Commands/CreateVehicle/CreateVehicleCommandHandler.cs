using DriveOps.Services.Services;
using FluentValidation;
using MediatR;

namespace DriveOps.Application.Commands.CreateVehicle
{
    public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, CreateVehicleResult>
    {
        private readonly IVehicleService _vehicleService;
        private readonly IValidator<CreateVehicleCommand> _validator;

        public CreateVehicleCommandHandler(
            IVehicleService vehicleService,
            IValidator<CreateVehicleCommand> validator)
        {
            _vehicleService = vehicleService;
            _validator = validator;
        }

        public async Task<CreateVehicleResult> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

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