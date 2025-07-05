using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;

namespace DriveOps.Models
{
    public class Truck : Vehicle
    {
        protected Truck() { }
        public Truck(ChassisId chassisId, string color)
            : base(chassisId, color)
        {
            Type = VehicleType.Truck;
            NumberOfPassengers = 1;
        }
    }
}
