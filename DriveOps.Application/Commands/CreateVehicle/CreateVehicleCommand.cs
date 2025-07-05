using DriveOps.Application.Commands.CreateVehicle;
using DriveOps.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<CreateVehicleResult>
    {
        public VehicleType Type { get; set; }
        public string ChassisSeries { get; set; } = string.Empty;
        public uint ChassisNumber { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}
