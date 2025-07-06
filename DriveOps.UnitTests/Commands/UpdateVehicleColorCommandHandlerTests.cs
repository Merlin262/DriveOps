using DriveOps.Application.Commands.UpdateVehicleColor;
using DriveOps.Enums;
using DriveOps.Models;
using DriveOps.Services.Services;
using DriveOps.UnitTests.Util;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DriveOps.UnitTests.Commands
{
    public class UpdateVehicleColorCommandHandlerTests
    {
        [Fact]
        internal async Task Handle_ReturnsFalse_WhenVehicleDoesNotExist()
        {
            // Arrange
            var service = Substitute.For<IVehicleService>();
            service.GetVehicleByChassisIdAsync("NOTFOUND", 999, Arg.Any<CancellationToken>())
                   .Returns((Vehicle?)null);

            var validator = Substitute.For<IValidator<UpdateVehicleColorCommand>>();
            validator.ValidateAsync(Arg.Any<UpdateVehicleColorCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new UpdateVehicleColorCommandHandler(service, validator);

            var command = new UpdateVehicleColorCommand
            {
                ChassisSeries = "NOTFOUND",
                ChassisNumber = 999,
                Color = "Azul"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            await service.Received(1).GetVehicleByChassisIdAsync("NOTFOUND", 999, Arg.Any<CancellationToken>());
        }

        [Fact]
        internal async Task Handle_ReturnsTrue_WhenVehicleExistsAndColorIsUpdated()
        {
            // Arrange
            var testVehicle = new TestVehicle("SERIE", 123, "Preto", VehicleType.Car, 4);
            var service = Substitute.For<IVehicleService>();
            service.GetVehicleByChassisIdAsync("SERIE", 123, Arg.Any<CancellationToken>())
                   .Returns(testVehicle);
            service.UpdateVehicleColorAsync(testVehicle, "Branco", Arg.Any<CancellationToken>())
                   .Returns(true);

            var validator = Substitute.For<IValidator<UpdateVehicleColorCommand>>();
            validator.ValidateAsync(Arg.Any<UpdateVehicleColorCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new UpdateVehicleColorCommandHandler(service, validator);

            var command = new UpdateVehicleColorCommand
            {
                ChassisSeries = "SERIE",
                ChassisNumber = 123,
                Color = "Branco"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            await service.Received(1).GetVehicleByChassisIdAsync("SERIE", 123, Arg.Any<CancellationToken>());
            await service.Received(1).UpdateVehicleColorAsync(testVehicle, "Branco", Arg.Any<CancellationToken>());
        }

        [Fact]
        internal async Task Handle_ThrowsValidationException_WhenValidationFails()
        {
            // Arrange
            var service = Substitute.For<IVehicleService>();
            var failures = new List<ValidationFailure> { new ValidationFailure("Color", "Invalid color") };
            var validator = Substitute.For<IValidator<UpdateVehicleColorCommand>>();
            validator.ValidateAsync(Arg.Any<UpdateVehicleColorCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(failures));

            var handler = new UpdateVehicleColorCommandHandler(service, validator);
            var command = new UpdateVehicleColorCommand
            {
                ChassisSeries = "SERIE",
                ChassisNumber = 123,
                Color = ""
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}