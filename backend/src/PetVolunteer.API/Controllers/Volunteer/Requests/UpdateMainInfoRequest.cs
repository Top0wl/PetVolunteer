using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateMainInfo;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record UpdateMainInfoRequest(
    FullNameDto FullNameDto,
    int Experience,
    string Email,
    string Description,
    string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId)
    {
        return new UpdateMainInfoCommand(
            volunteerId,
            FullNameDto,
            Experience,
            Email,
            Description,
            PhoneNumber);
    }
}