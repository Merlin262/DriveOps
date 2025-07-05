using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.UpdateVehicleColor
{
    public class UpdateVehicleColorCommand : IRequest<bool>
    {
        public string ChassisSeries { get; set; }
        public uint ChassisNumber { get; set; }
        public string Color { get; set; }
    }
}
