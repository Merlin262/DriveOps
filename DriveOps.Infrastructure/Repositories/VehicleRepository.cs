using DriveOps.Domain.Repositories;
using DriveOps.Infrastructure.Data.Context;
using DriveOps.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> AddAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles.ToListAsync(cancellationToken);
        }
        public async Task<Vehicle?> GetByChassisIdAsync(string chassisSeries, uint chassisNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.ChassisSeries == chassisSeries && v.ChassisNumber == chassisNumber, cancellationToken);
        }
        public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken = default)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
