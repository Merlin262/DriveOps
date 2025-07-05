using DriveOps.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.CreateVehicle
{
    public class CreateVehicleResult
    {
        public ChassisId ChassisId { get; set; }
    }
}
