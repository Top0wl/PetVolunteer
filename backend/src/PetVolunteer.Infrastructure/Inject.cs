using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using PetVolunteer.Application;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Application.Volunteer;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Infrastructure.BackgroundServices;
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
        services.AddScoped<ApplicationDbContext>(); 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        //Repositories
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        
        //BackgroundSerivce
        services.AddHostedService<FilesCleanerBackgroundService>();
        
        //Queues
        services.AddSingleton<IMessageQueue<IEnumerable<(FilePath, string)>>, InMemoryMessageQueue<IEnumerable<(FilePath, string)>>>();
        
        //MinIO
        services.AddMinio(configuration);
        
        return services;
    }

    private static IServiceCollection AddMinio(
        this IServiceCollection services, IConfiguration configuration)
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