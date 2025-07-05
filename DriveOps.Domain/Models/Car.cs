using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;

namespace DriveOps.Models
{
    public class Car : Vehicle
    {
        protected Car() { }
        public Car(ChassisId chassisId, string color)
            : base(chassisId, color)
        {
            Type = VehicleType.Car;
            NumberOfPassengers = 4;
        }
    }
}
