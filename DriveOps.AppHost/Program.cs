var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.DriveOps>("driveops");

builder.Build().Run();
