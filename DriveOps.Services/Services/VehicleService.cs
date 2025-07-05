using DriveOps.Domain.Repositories;
using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveOps.Services.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<ChassisId> CreateVehicleAsync(
            VehicleType type,
            string chassisSeries,
            uint chassisNumber,
            string color,
            CancellationToken cancellationToken = default)
        {
            const string duplicatedChassiId = "Cannot insert duplicate key";
            var chassisId = new ChassisId(chassisSeries, chassisNumber);

            Vehicle vehicle = type switch
            {
                VehicleType.Car => new Car(chassisId, color),
                VehicleType.Bus => new Bus(chassisId, color),
                VehicleType.Truck => new Truck(chassisId, color),
                _ => throw new ArgumentException("Tipo de veículo inválido")
            };

            try
            {
                await _vehicleRepository.AddAsync(vehicle, cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message.Contains(duplicatedChassiId) == true)
            {
                throw new InvalidOperationException("Já existe um veículo com este ChassisId.");
            }

            return vehicle.ChassisId;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _vehicleRepository.GetAllAsync(cancellationToken);
        }
        public async Task<Vehicle?> GetVehicleByChassisIdAsync(string chassisSeries, uint chassisNumber, CancellationToken cancellationToken = default)
        {
            return await _vehicleRepository.GetByChassisIdAsync(chassisSeries, chassisNumber, cancellationToken);
        }

        public async Task<bool> UpdateVehicleColorAsync(Vehicle vehicle, string color, CancellationToken cancellationToken = default)
        {
            vehicle.Color = color;
            await _vehicleRepository.UpdateAsync(vehicle, cancellationToken);
            return true;
        }
    }
}