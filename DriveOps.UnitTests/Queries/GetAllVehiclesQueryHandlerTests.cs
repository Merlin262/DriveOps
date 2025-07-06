using DriveOps.Application.Queries.GetAllVehicles;
using DriveOps.Domain.Repositories;
using DriveOps.Enums;
using DriveOps.Models;
using DriveOps.UnitTests.Util;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DriveOps.UnitTests.Queries
{
    public class GetAllVehiclesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsMappedResults_WhenVehiclesExist()
        {
            // Arrange
            var vehicles = new List<Vehicle>
            {
                new TestVehicle("ABC", 123, "Azul", VehicleType.Car, 4),
                new TestVehicle("XYZ", 456, "Vermelho", VehicleType.Bus, 40)
            };

            var repository = Substitute.For<IVehicleRepository>();
            repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(vehicles);

            var handler = new GetAllVehiclesQueryHandler(repository);
            var query = new GetAllVehiclesQuery();

            // Act
            var result = (await handler.Handle(query, CancellationToken.None)).ToList();

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Equal("ABC", result[0].ChassisSeries);
            Assert.Equal((uint)123, result[0].ChassisNumber);
            Assert.Equal("Azul", result[0].Color);
            Assert.Equal(VehicleType.Car, result[0].Type);
            Assert.Equal(4, result[0].NumberOfPassengers);

            Assert.Equal("XYZ", result[1].ChassisSeries);
            Assert.Equal((uint)456, result[1].ChassisNumber);
            Assert.Equal("Vermelho", result[1].Color);
            Assert.Equal(VehicleType.Bus, result[1].Type);
            Assert.Equal(40, result[1].NumberOfPassengers);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoVehiclesExist()
        {
            // Arrange
            var repository = Substitute.For<IVehicleRepository>();
            repository.GetAllAsync(Arg.Any<CancellationToken>()).Returns(new List<Vehicle>());

            var handler = new GetAllVehiclesQueryHandler(repository);
            var query = new GetAllVehiclesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }
    }
}