using DriveOps.Application.Commands.CreateVehicle;
using DriveOps.Application.Commands.UpdateVehicleColor;
using DriveOps.Application.Queries.GetVehicleByChassisId;
using DriveOps.Domain.Repositories;
using DriveOps.Infrastructure.Repositories;
using DriveOps.Services.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DriveOps.DependencyInjection
{
    internal static class DependencyInjection
    {
        internal static IServiceCollection AddDriveOpsServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateVehicleCommandHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IValidator<CreateVehicleCommand>, CreateVehicleCommandValidator>();
            services.AddScoped<IValidator<UpdateVehicleColorCommand>, UpdateVehicleColorCommandValidator>();
            services.AddScoped<IValidator<GetVehicleByChassisIdQuery>, GetVehicleByChassisIdQueryValidator>();
            return services;
        }
    }
}
