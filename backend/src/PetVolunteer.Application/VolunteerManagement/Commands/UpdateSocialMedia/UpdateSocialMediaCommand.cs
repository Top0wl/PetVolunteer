using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateSocialMedia;

public record UpdateSocialMediaCommand(Guid VolunteerId, IEnumerable<SocialMediaDto> SocialMedia) : ICommand;