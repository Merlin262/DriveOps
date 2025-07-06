using DriveOps.Application.Commands.CreateVehicle;
using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Services.Services;
using DriveOps.UnitTests.Util;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using System.Threading.Tasks;

namespace DriveOps.UnitTests.Commands
{
    public class CreateVehicleCommandHandlerTests
    {
        [Fact]
        internal async Task Handle_ReturnsResultWithChassisId_WhenVehicleIsCreated()
        {
            // Arrange
            var expectedChassisId = new ChassisId("SERIE", 123);
            var service = Substitute.For<IVehicleService>();
            service.CreateVehicleAsync(
                VehicleType.Car, "SERIE", 123, "Azul", Arg.Any<CancellationToken>())
                .Returns(expectedChassisId);

            var validator = Substitute.For<IValidator<CreateVehicleCommand>>();
            validator.ValidateAsync(Arg.Any<CreateVehicleCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new CreateVehicleCommandHandler(service, validator);

            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Car,
                ChassisSeries = "SERIE",
                ChassisNumber = 123,
                Color = "Azul"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.ChassisId);
            Assert.Equal(expectedChassisId.Series, result.ChassisId.Series);
            Assert.Equal(expectedChassisId.Number, result.ChassisId.Number);
        }

        [Fact]
        internal async Task Handle_PassesCorrectParametersToService()
        {
            // Arrange
            var service = Substitute.For<IVehicleService>();
            service.CreateVehicleAsync(
                Arg.Any<VehicleType>(), Arg.Any<string>(), Arg.Any<uint>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
                .Returns(call => new ChassisId(
                    call.ArgAt<string>(1),
                    call.ArgAt<uint>(2)
                ));

            var validator = Substitute.For<IValidator<CreateVehicleCommand>>();
            validator.ValidateAsync(Arg.Any<CreateVehicleCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult());

            var handler = new CreateVehicleCommandHandler(service, validator);

            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Bus,
                ChassisSeries = "BUS123",
                ChassisNumber = 999,
                Color = "Vermelho"
            };

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            await service.Received(1).CreateVehicleAsync(
                VehicleType.Bus, "BUS123", 999, "Vermelho", Arg.Any<CancellationToken>());
        }

        [Fact]
        internal async Task Handle_ThrowsValidationException_WhenValidationFails()
        {
            // Arrange
            var service = Substitute.For<IVehicleService>();
            var failures = new List<ValidationFailure> { new ValidationFailure("Type", "Invalid type") };
            var validator = Substitute.For<IValidator<CreateVehicleCommand>>();
            validator.ValidateAsync(Arg.Any<CreateVehicleCommand>(), Arg.Any<CancellationToken>())
                .Returns(new ValidationResult(failures));

            var handler = new CreateVehicleCommandHandler(service, validator);
            var command = new CreateVehicleCommand
            {
                Type = (VehicleType)999,
                ChassisSeries = "SERIE",
                ChassisNumber = 123,
                Color = "Azul"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}