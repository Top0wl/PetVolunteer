using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetVolunteer.Application.Providers.FileProvider;

namespace PetVolunteer.Infrastructure.BackgroundServices;

public class FilesCleanerBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    
    public FilesCleanerBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //while (!stoppingToken.IsCancellationRequested)
        //{
            //await using var scope = _scopeFactory.CreateAsyncScope();
            //var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
        //}
        //throw new NotImplementedException();
    }
}