using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Database;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Infrastructure.Interceptors;

namespace PetVolunteer.Infrastructure;

public class ApplicationDbContext(
    IConfiguration configuration) : DbContext, IApplicationDbContext
{
    private const string DATABASE = "Database";
    public DbSet<Volunteer> Volunteers { get; set; }
    
    private ILoggerFactory CreateLoggerFactory() => LoggerFactory.Create(builder => builder.AddConsole());
    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return await base.Database.BeginTransactionAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(configuration.GetConnectionString(DATABASE));
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory()); 
        optionsBuilder.EnableSensitiveDataLogging();
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        optionsBuilder.AddInterceptors(new SoftDeleteInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}