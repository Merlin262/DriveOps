using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;

namespace DriveOps.Models
{
    public class Bus : Vehicle
    {
        protected Bus() { }

        public Bus(ChassisId chassisId, string color)
            : base(chassisId, color)
        {
            Type = VehicleType.Bus;
            NumberOfPassengers = 42;
        }
    }
}
