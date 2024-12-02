using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Infrastructure.DbContexts;

public class ReadDbContext(IConfiguration configuration) : DbContext, IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers => Set<VolunteerDto>().AsQueryable();
    public IQueryable<PetDto> Pets => Set<PetDto>().AsQueryable();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(Constants.DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory()); 
        optionsBuilder.EnableSensitiveDataLogging();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);
    }
    
    private ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(builder => builder.AddConsole());
}