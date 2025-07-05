using DriveOps.Application.Queries.GetAllVehicles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Queries.GetVehicleByChassisId
{
    public class GetVehicleByChassisIdQuery : IRequest<GetAllVehiclesQueryHandlerResult>
    {
        public string ChassisSeries { get; }
        public uint ChassisNumber { get; }

        public GetVehicleByChassisIdQuery(string chassisSeries, uint chassisNumber)
        {
            ChassisSeries = chassisSeries;
            ChassisNumber = chassisNumber;
        }
    }
}
