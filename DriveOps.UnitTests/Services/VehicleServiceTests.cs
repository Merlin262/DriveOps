using DriveOps.Domain.Repositories;
using DriveOps.Enums;
using DriveOps.Models;
using DriveOps.Services.Services;
using DriveOps.UnitTests.Util;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DriveOps.UnitTests.Services
{
    public class VehicleServiceTests
    {
        [Fact]
        public async Task CreateVehicleAsync_CreatesCarAndReturnsChassisId()
        {
            // Arrange
            var repo = Substitute.For<IVehicleRepository>();
            repo.AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>())
                .Returns(call => call.Arg<Vehicle>());
            var service = new VehicleService(repo);

            // Act
            var chassisId = await service.CreateVehicleAsync(VehicleType.Car, "CAR", 1, "Azul");

            // Assert
            Assert.NotNull(chassisId);
            Assert.Equal("CAR", chassisId.Series);
            Assert.Equal((uint)1, chassisId.Number);
            await repo.Received(1).AddAsync(Arg.Is<Vehicle>(v =>
                v.Type == VehicleType.Car &&
                v.ChassisSeries == "CAR" &&
                v.ChassisNumber == 1 &&
                v.Color == "Azul"
            ), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task CreateVehicleAsync_CreatesBusAndReturnsChassisId()
        {
            var repo = Substitute.For<IVehicleRepository>();
            repo.AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>())
                .Returns(call => call.Arg<Vehicle>());
            var service = new VehicleService(repo);

            var chassisId = await service.CreateVehicleAsync(VehicleType.Bus, "BUS", 2, "Vermelho");

            Assert.NotNull(chassisId);
            Assert.Equal("BUS", chassisId.Series);
            Assert.Equal((uint)2, chassisId.Number);
            await repo.Received(1).AddAsync(Arg.Is<Vehicle>(v =>
                v.Type == VehicleType.Bus &&
                v.ChassisSeries == "BUS" &&
                v.ChassisNumber == 2 &&
                v.Color == "Vermelho"
            ), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task CreateVehicleAsync_CreatesTruckAndReturnsChassisId()
        {
            var repo = Substitute.For<IVehicleRepository>();
            repo.AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>())
                .Returns(call => call.Arg<Vehicle>());
            var service = new VehicleService(repo);

            var chassisId = await service.CreateVehicleAsync(VehicleType.Truck, "TRK", 3, "Preto");

            Assert.NotNull(chassisId);
            Assert.Equal("TRK", chassisId.Series);
            Assert.Equal((uint)3, chassisId.Number);
            await repo.Received(1).AddAsync(Arg.Is<Vehicle>(v =>
                v.Type == VehicleType.Truck &&
                v.ChassisSeries == "TRK" &&
                v.ChassisNumber == 3 &&
                v.Color == "Preto"
            ), Arg.Any<CancellationToken>());
        }

        [Fact]
        public async Task CreateVehicleAsync_ThrowsArgumentException_WhenTypeIsInvalid()
        {
            var repo = Substitute.For<IVehicleRepository>();
            var service = new VehicleService(repo);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.CreateVehicleAsync((VehicleType)999, "INV", 4, "Branco"));
        }

        [Fact]
        public async Task CreateVehicleAsync_ThrowsInvalidOperationException_OnDuplicateChassisId()
        {
            // Arrange
            var repo = Substitute.For<IVehicleRepository>();
            repo.AddAsync(Arg.Any<Vehicle>(), Arg.Any<CancellationToken>())
                .Returns<Task<Vehicle>>(x => throw new Microsoft.EntityFrameworkCore.DbUpdateException("Erro", new Exception("Cannot insert duplicate key")));
            var service = new VehicleService(repo);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CreateVehicleAsync(VehicleType.Car, "DUP", 5, "Azul"));
            Assert.Contains("Já existe um veículo com este ChassisId", ex.Message);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new TestVehicle("A", 1, "Azul", VehicleType.Car, 4),
                new TestVehicle("B", 2, "Vermelho", VehicleType.Bus, 40)
            };
            var repo = Substitute.For<IVehicleRepository>();
            repo.GetAllAsync(Arg.Any<CancellationToken>()).Returns(vehicles);
            var service = new VehicleService(repo);

            var result = await service.GetAllAsync();

            Assert.Equal(2, ((List<Vehicle>)result).Count);
        }

        [Fact]
        public async Task GetVehicleByChassisIdAsync_ReturnsVehicle_WhenExists()
        {
            var vehicle = new TestVehicle("A", 1, "Azul", VehicleType.Car, 4);
            var repo = Substitute.For<IVehicleRepository>();
            repo.GetByChassisIdAsync("A", 1, Arg.Any<CancellationToken>()).Returns(vehicle);
            var service = new VehicleService(repo);

            var result = await service.GetVehicleByChassisIdAsync("A", 1);

            Assert.NotNull(result);
            Assert.Equal(vehicle, result);
        }

        [Fact]
        public async Task GetVehicleByChassisIdAsync_ReturnsNull_WhenNotExists()
        {
            var repo = Substitute.For<IVehicleRepository>();
            repo.GetByChassisIdAsync("X", 99, Arg.Any<CancellationToken>()).Returns((Vehicle?)null);
            var service = new VehicleService(repo);

            var result = await service.GetVehicleByChassisIdAsync("X", 99);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateVehicleColorAsync_UpdatesColorAndReturnsTrue()
        {
            var vehicle = new TestVehicle("A", 1, "Azul", VehicleType.Car, 4);
            var repo = Substitute.For<IVehicleRepository>();
            repo.UpdateAsync(vehicle, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);
            var service = new VehicleService(repo);

            var result = await service.UpdateVehicleColorAsync(vehicle, "Verde");

            Assert.True(result);
            Assert.Equal("Verde", vehicle.Color);
            await repo.Received(1).UpdateAsync(vehicle, Arg.Any<CancellationToken>());
        }
    }
}