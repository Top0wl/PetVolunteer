using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
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
        services.AddScoped<CreateVolunteerHandler>();
        services.AddScoped<UpdateMainInfoHandler>();
        services.AddScoped<DeleteVolunteerHandler>();
        services.AddScoped<UpdateRequisitesHandler>();
        services.AddScoped<UpdateSocialMediaHandler>();
        services.AddScoped<AddPetHandler>();
        services.AddScoped<UploadFilesToPetHandler>();

        services.AddScoped<GetVolunteersWithPaginationHandler>();
        
        services.AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }
}