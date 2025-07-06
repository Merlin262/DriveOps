using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveOps.Application.Commands.CreateVehicle
{
    public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
    {
        public CreateVehicleCommandValidator()
        {
            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Type deve ser um valor válido do enum VehicleType.");

            RuleFor(x => x.ChassisSeries)
                .NotEmpty().WithMessage("ChassisSeries não pode ser nulo ou vazio.");

            RuleFor(x => x.ChassisNumber)
                .InclusiveBetween(100000u, 999999u)
                .WithMessage("ChassisNumber deve conter exatamente 6 dígitos.");

            RuleFor(x => x.Color)
                .NotEmpty().WithMessage("Color não pode ser nulo ou vazio.");
        }
    }
}
