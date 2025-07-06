using DriveOps.Application.Queries.GetVehicleByChassisId;
using DriveOps.Domain.Repositories;
using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Models;
using DriveOps.UnitTests.Util;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DriveOps.UnitTests.Queries
{
    public class GetVehicleByChassisIdQueryHandlerTests
    {
        [Fact]
        internal async Task Handle_ReturnsMappedResult_WhenVehicleExists()
        {
            // Arrange
            var vehicle = new TestVehicle("ABC", 123, "Azul", VehicleType.Car, 4);
            var repository = Substitute.For<IVehicleRepository>();
            repository.GetByChassisIdAsync("ABC", 123, Arg.Any<CancellationToken>())
                      .Returns(vehicle);

            var validator = Substitute.For<IValidator<GetVehicleByChassisIdQuery>>();
            validator.ValidateAsync(Arg.Any<GetVehicleByChassisIdQuery>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new GetVehicleByChassisIdQueryHandler(repository, validator);
            var query = new GetVehicleByChassisIdQuery("ABC", 123);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("ABC", result.ChassisSeries);
            Assert.Equal((uint)123, result.ChassisNumber);
            Assert.Equal("Azul", result.Color);
            Assert.Equal(VehicleType.Car, result.Type);
            Assert.Equal(4, result.NumberOfPassengers);
        }

        [Fact]
        internal async Task Handle_ReturnsNull_WhenVehicleDoesNotExist()
        {
            // Arrange
            var repository = Substitute.For<IVehicleRepository>();
            repository.GetByChassisIdAsync("ZZZ", 999, Arg.Any<CancellationToken>())
                      .Returns((Vehicle?)null);

            var validator = Substitute.For<IValidator<GetVehicleByChassisIdQuery>>();
            validator.ValidateAsync(Arg.Any<GetVehicleByChassisIdQuery>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new GetVehicleByChassisIdQueryHandler(repository, validator);
            var query = new GetVehicleByChassisIdQuery("ZZZ", 999);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        internal async Task Handle_ThrowsValidationException_WhenValidationFails()
        {
            // Arrange
            var repository = Substitute.For<IVehicleRepository>();
            var failures = new List<ValidationFailure> { new ValidationFailure("ChassisSeries", "Invalid series") };
            var validator = Substitute.For<IValidator<GetVehicleByChassisIdQuery>>();
            validator.ValidateAsync(Arg.Any<GetVehicleByChassisIdQuery>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(failures));

            var handler = new GetVehicleByChassisIdQueryHandler(repository, validator);
            var query = new GetVehicleByChassisIdQuery("", 123);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(query, CancellationToken.None));
        }
    }
}