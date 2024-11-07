using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Volunteer.UpdateSocialMedia;

public record UpdateSocialMediaCommand(Guid VolunteerId, IEnumerable<SocialMediaDto> SocialMedia);