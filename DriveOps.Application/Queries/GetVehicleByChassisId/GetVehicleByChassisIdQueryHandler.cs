using DriveOps.Application.Queries.GetAllVehicles;
using DriveOps.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Queries.GetVehicleByChassisId
{
    public class GetVehicleByChassisIdQueryHandler : IRequestHandler<GetVehicleByChassisIdQuery, GetAllVehiclesQueryHandlerResult>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetVehicleByChassisIdQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<GetAllVehiclesQueryHandlerResult> Handle(GetVehicleByChassisIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _vehicleRepository.GetByChassisIdAsync(request.ChassisSeries, request.ChassisNumber);
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
}
