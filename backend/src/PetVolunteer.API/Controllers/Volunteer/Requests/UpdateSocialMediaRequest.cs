using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Volunteer.UpdateSocialMedia;

namespace PetVolunteer.API.Controllers.Requests;

public record UpdateSocialMediaRequest(IEnumerable<SocialMediaDto> SocialMedia)
{
    public UpdateSocialMediaCommand ToCommand(Guid volunteerId)
    {
        return new UpdateSocialMediaCommand(volunteerId, SocialMedia);
    }
}