using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PetVolunteer.Application.Volunteer.AddPet;
using PetVolunteer.Application.Volunteer.CreateVolunteer;
using PetVolunteer.Application.Volunteer.Detele;
using PetVolunteer.Application.Volunteer.UpdateMainInfo;
using PetVolunteer.Application.Volunteer.UpdateRequisites;
using PetVolunteer.Application.Volunteer.UpdateSocialMedia;

namespace PetVolunteer.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<UpdateRequisitesHandler>();
        services.AddScoped<UpdateSocialMediaHandler>();
        services.AddScoped<AddPetHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}