using Microsoft.EntityFrameworkCore;

namespace DriveOps.Domain.ValueObjects
{
    [Owned]
    public class ChassisId
    {
        public string Series { get; }
        public uint Number { get; }

        public ChassisId(string series, uint number)
        {
            Series = series;
            Number = number;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ChassisId other)
                return false;
            return Series == other.Series && Number == other.Number;
        }

        public override int GetHashCode() => HashCode.Combine(Series, Number);
    }
}
