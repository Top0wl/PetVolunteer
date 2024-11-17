using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateSocialMedia;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record UpdateSocialMediaRequest(IEnumerable<SocialMediaDto> SocialMedia)
{
    public UpdateSocialMediaCommand ToCommand(Guid volunteerId)
    {
        return new UpdateSocialMediaCommand(volunteerId, SocialMedia);
    }
}