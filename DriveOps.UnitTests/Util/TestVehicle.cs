using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.UnitTests.Util
{
    internal class TestVehicle : Vehicle
    {
        internal TestVehicle(string chassisSeries, uint chassisNumber, string color, VehicleType type, int numberOfPassengers)
            : base(new ChassisId(chassisSeries, chassisNumber), color)
        {
            base.Type = type;
            base.NumberOfPassengers = numberOfPassengers;
            base.ChassisSeries = chassisSeries;
            base.ChassisNumber = chassisNumber;
            base.Color = color;
        }
    }
}
