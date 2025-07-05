using DriveOps.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Queries.GetAllVehicles
{
    public class GetAllVehiclesQueryHandler : IRequestHandler<GetAllVehiclesQuery, IEnumerable<GetAllVehiclesQueryHandlerResult>>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public GetAllVehiclesQueryHandler(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<GetAllVehiclesQueryHandlerResult>> Handle(GetAllVehiclesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = await _vehicleRepository.GetAllAsync();

            return vehicles.Select(v => new GetAllVehiclesQueryHandlerResult
            {
                ChassisSeries = v.ChassisSeries,
                ChassisNumber = v.ChassisNumber,
                Color = v.Color,
                Type = v.Type,
                NumberOfPassengers = v.NumberOfPassengers
            });
        }
    }
}
