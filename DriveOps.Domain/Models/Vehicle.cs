using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;

namespace DriveOps.Models
{
    public abstract class Vehicle
    {
        public string ChassisSeries { get; protected set; }
        public uint ChassisNumber { get; protected set; }

        public ChassisId ChassisId => new ChassisId(ChassisSeries, ChassisNumber);

        public VehicleType Type { get; protected set; }
        public int NumberOfPassengers { get; protected set; }
        public string Color { get; set; }

        protected Vehicle(ChassisId chassisId, string color)
        {
            ChassisSeries = chassisId.Series;
            ChassisNumber = chassisId.Number;
            Color = color;
        }

        protected Vehicle() { }
    }
}
