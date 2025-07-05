var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DriveOps_Api>("driveops");

builder.Build().Run();
