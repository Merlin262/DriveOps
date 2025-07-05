using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Models;
using System.Threading;
using System.Threading.Tasks;

namespace DriveOps.Services.Services
{
    public interface IVehicleService
    {
        Task<ChassisId> CreateVehicleAsync(
            VehicleType type,
            string chassisSeries,
            uint chassisNumber,
            string color,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Vehicle?> GetVehicleByChassisIdAsync(string chassisSeries, uint chassisNumber, CancellationToken cancellationToken = default);
        Task<bool> UpdateVehicleColorAsync(Vehicle vehicle, string color, CancellationToken cancellationToken = default);
    }
}
