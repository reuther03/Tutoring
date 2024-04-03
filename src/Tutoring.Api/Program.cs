using Tutoring.Application;
using Tutoring.Domain;
using Tutoring.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


#region services

var services = builder.Services;

services
    .AddDomain(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

#endregion

#region app

var app = builder.Build();

app.UseInfrastructure();
app.Run();

#endregion