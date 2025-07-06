using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace DriveOps.Application.Queries.GetVehicleByChassisId
{
    public class GetVehicleByChassisIdQueryValidator : AbstractValidator<GetVehicleByChassisIdQuery>
    {
        public GetVehicleByChassisIdQueryValidator()
        {
            RuleFor(x => x.ChassisSeries)
                .NotEmpty().WithMessage("ChassisSeries não pode ser nulo ou vazio.");

            RuleFor(x => x.ChassisNumber)
                .InclusiveBetween(100000u, 999999u)
                .WithMessage("ChassisNumber deve conter exatamente 6 dígitos.");
        }
    }
}
