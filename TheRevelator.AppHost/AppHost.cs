var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.TheRevelator>("therevelator");

builder.AddProject<Projects.BeyondTheSportlight>("beyoundthespotlight");

builder.AddProject<Projects.BeyondTheSportlight>("beyondthesportlight");

builder.Build().Run();
