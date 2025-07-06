using DriveOps.Application.Queries.GetAllVehicles;
using DriveOps.Application.Queries.GetVehicleByChassisId;
using DriveOps.Domain.Repositories;
using FluentValidation;
using MediatR;

public class GetVehicleByChassisIdQueryHandler : IRequestHandler<GetVehicleByChassisIdQuery, GetAllVehiclesQueryHandlerResult>
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IValidator<GetVehicleByChassisIdQuery> _validator;

    public GetVehicleByChassisIdQueryHandler(
        IVehicleRepository vehicleRepository,
        IValidator<GetVehicleByChassisIdQuery> validator)
    {
        _vehicleRepository = vehicleRepository;
        _validator = validator;
    }

    public async Task<GetAllVehiclesQueryHandlerResult> Handle(GetVehicleByChassisIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var vehicle = await _vehicleRepository.GetByChassisIdAsync(request.ChassisSeries, request.ChassisNumber, cancellationToken);
        if (vehicle == null)
            return null;

        return new GetAllVehiclesQueryHandlerResult
        {
            ChassisSeries = vehicle.ChassisSeries,
            ChassisNumber = vehicle.ChassisNumber,
            Color = vehicle.Color,
            Type = vehicle.Type,
            NumberOfPassengers = vehicle.NumberOfPassengers
        };
    }
}