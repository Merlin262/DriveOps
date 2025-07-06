using DriveOps.Application.Commands.CreateVehicle;
using DriveOps.Enums;
using FluentValidation.TestHelper;
using Xunit;

namespace DriveOps.UnitTests.Validators
{
    public class CreateVehicleCommandValidatorTests
    {
        private readonly CreateVehicleCommandValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void Should_Have_Error_When_ChassisSeries_Is_Null_Or_Empty(string chassisSeries)
        {
            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Car,
                ChassisSeries = chassisSeries!,
                ChassisNumber = 123456,
                Color = "Blue"
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ChassisSeries);
        }

        [Theory]
        [InlineData(99999)]
        [InlineData(1000000)]
        internal void Should_Have_Error_When_ChassisNumber_Is_Out_Of_Range(uint chassisNumber)
        {
            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Car,
                ChassisSeries = "ABC",
                ChassisNumber = chassisNumber,
                Color = "Blue"
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.ChassisNumber);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void Should_Have_Error_When_Color_Is_Null_Or_Empty(string color)
        {
            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Car,
                ChassisSeries = "ABC",
                ChassisNumber = 123456,
                Color = color!
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Color);
        }

        [Fact]
        internal void Should_Have_Error_When_Type_Is_Invalid()
        {
            var command = new CreateVehicleCommand
            {
                Type = (VehicleType)999,
                ChassisSeries = "ABC",
                ChassisNumber = 123456,
                Color = "Blue"
            };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Type);
        }

        [Fact]
        internal void Should_Not_Have_Error_When_Data_Is_Valid()
        {
            var command = new CreateVehicleCommand
            {
                Type = VehicleType.Bus,
                ChassisSeries = "DEF",
                ChassisNumber = 654321,
                Color = "Red"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
