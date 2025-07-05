using DriveOps.Domain.ValueObjects;
using DriveOps.Enums;
using DriveOps.Models;
using Microsoft.EntityFrameworkCore;

namespace DriveOps.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                .HasIndex(v => new { v.ChassisSeries, v.ChassisNumber })
                .IsUnique();

            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.HasKey(v => new { v.ChassisSeries, v.ChassisNumber });

                vehicle.HasDiscriminator<VehicleType>(v => v.Type)
                    .HasValue<Car>(VehicleType.Car)
                    .HasValue<Bus>(VehicleType.Bus)
                    .HasValue<Truck>(VehicleType.Truck);

                vehicle.Ignore(v => v.ChassisId);
            });
        }
    }
}