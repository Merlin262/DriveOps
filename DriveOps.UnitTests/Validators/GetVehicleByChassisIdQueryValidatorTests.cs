using DriveOps.Application.Queries.GetVehicleByChassisId;
using FluentValidation.TestHelper;
using Xunit;

namespace DriveOps.UnitTests.Validators
{
    public class GetVehicleByChassisIdQueryValidatorTests
    {
        private readonly GetVehicleByChassisIdQueryValidator _validator = new();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        internal void Should_Have_Error_When_ChassisSeries_Is_Null_Or_Empty(string chassisSeries)
        {
            var query = new GetVehicleByChassisIdQuery(chassisSeries, 123456);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.ChassisSeries);
        }

        [Theory]
        [InlineData(99999)]
        [InlineData(1000000)]
        internal void Should_Have_Error_When_ChassisNumber_Is_Out_Of_Range(uint chassisNumber)
        {
            var query = new GetVehicleByChassisIdQuery("ABC", chassisNumber);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.ChassisNumber);
        }

        [Fact]
        internal void Should_Not_Have_Error_When_Data_Is_Valid()
        {
            var query = new GetVehicleByChassisIdQuery("ABC", 123456);
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
