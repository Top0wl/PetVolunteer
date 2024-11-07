using Microsoft.AspNetCore.Diagnostics;
using PetVolunteer.API;
using PetVolunteer.API.Extensions;
using PetVolunteer.API.Middlewares;
using PetVolunteer.API.Validation;
using PetVolunteer.Application;
using PetVolunteer.Application.Volunteer;
using PetVolunteer.Application.Volunteer.CreateVolunteer;
using PetVolunteer.Infrastructure;
using PetVolunteer.Infrastructure.Repositories;
using Serilog;
using Serilog.Events;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Debug()
    .WriteTo.Seq(builder.Configuration.GetConnectionString("Seq") 
                 ?? throw new ArgumentNullException("Seq in appsettings not found"))
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
    .CreateLogger();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSerilog();

builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

// builder.Services.AddFluentValidationAutoValidation(configuration =>
// {
//     configuration.OverrideDefaultResultFactoryWith<CustomResultFactory>();
// });

var app = builder.Build();

app.UseExceptionMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.ApplyMigrations();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();