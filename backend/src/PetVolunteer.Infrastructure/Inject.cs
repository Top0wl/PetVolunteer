using Microsoft.Extensions.DependencyInjection;
using PetVolunteer.Application.Volunteer;
using PetVolunteer.Infrastructure.Interceptors;
using PetVolunteer.Infrastructure.Repositories;

namespace PetVolunteer.Infrastructure;

public static class Inject
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ApplicationDbContext>();
        
        //Repositories
        services.AddScoped<IVolunteerRepository, VolunteerRepository>();
        
        return services;
    }
}