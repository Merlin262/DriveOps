using DriveOps.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Queries.GetAllVehicles
{
    public class GetAllVehiclesQueryHandlerResult
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public string Color { get; set; }
        public VehicleType Type { get; set; }
        public int NumberOfPassengers { get; set; }
    }
}
