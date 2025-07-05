using DriveOps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> AddAsync(Vehicle vehicle, CancellationToken cancellationToken);
        Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Vehicle?> GetByChassisIdAsync(string chassisSeries, uint chassisNumber, CancellationToken cancellationToken = default);
        Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default);
    }
}
