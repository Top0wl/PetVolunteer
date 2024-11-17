using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.VolunteerManagement.Commands.CreateVolunteer;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record CreateVolunteerRequest(
    FullNameDto FullName,
    string Email,
    string Description,
    string PhoneNumber,
    int ExperienceOfWork)
{
    public CreateVolunteerCommand ToCommand()
    {
        return new CreateVolunteerCommand(FullName, Email, Description, PhoneNumber, ExperienceOfWork);
    }
}
    