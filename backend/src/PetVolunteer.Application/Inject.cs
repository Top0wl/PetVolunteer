using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.VolunteerManagement.Commands.AddPet;
using PetVolunteer.Application.VolunteerManagement.Commands.CreateVolunteer;
using PetVolunteer.Application.VolunteerManagement.Commands.Detele;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateRequisites;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateSocialMedia;
using PetVolunteer.Application.VolunteerManagement.Commands.UploadFilesToPet;
using PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetVolunteer.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddCommandHandlers()
            .AddQueryHandlers()
            .AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }

    private static IServiceCollection AddCommandHandlers(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(c => c.AssignableToAny(typeof(ICommandHandler<>), typeof(ICommandHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
    
    private static IServiceCollection AddQueryHandlers(this IServiceCollection services)
    {
        return services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime());
    }
}