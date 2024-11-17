using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetVolunteer.Application;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.Messaging;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Application.VolunteerManagement;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Infrastructure.BackgroundServices;
using PetVolunteer.Infrastructure.DbContexts;
using PetVolunteer.Infrastructure.Interceptors;
using PetVolunteer.Infrastructure.MessageQueues;
using PetVolunteer.Infrastructure.Providers;
using PetVolunteer.Infrastructure.Repositories;
using MinioOptions = PetVolunteer.Infrastructure.Options.MinioOptions;

namespace PetVolunteer.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddDbContexts()
            .AddMinio(configuration)
            .AddRepositories()
            .AddHostedServices()
            .AddMessageQueues();
        
        return services;
    }
    
    private static IServiceCollection AddMessageQueues(this IServiceCollection services)
    {
        services.AddSingleton<IMessageQueue<IEnumerable<(FilePath, string)>>, InMemoryMessageQueue<IEnumerable<(FilePath, string)>>>();
        return services;
    }

    private static IServiceCollection AddHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<FilesCleanerBackgroundService>();
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        return services;
    }
    
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
    
    private static IServiceCollection AddMinio(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MinioOptions>(configuration.GetSection(MinioOptions.MINIO));
        //TODO: изучить как сделать по другому
        services.AddMinio(options =>
        {
            var minioOptions = configuration.GetSection(MinioOptions.MINIO).Get<MinioOptions>() 
                               ?? throw new ApplicationException("Missing minio configuration");
            
            options.WithEndpoint(minioOptions.Endpoint);
            options.WithCredentials(minioOptions.AccessKey, minioOptions.SecretKey);
            options.WithSSL(minioOptions.UseSsl);
        });

        services.AddScoped<IFileProvider, MinioProvider>();
        
        return services;
    }
}