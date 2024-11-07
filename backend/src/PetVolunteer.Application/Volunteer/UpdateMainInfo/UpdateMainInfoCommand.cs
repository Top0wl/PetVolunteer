using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public record UpdateMainInfoCommand(Guid VolunteerId, FullNameDto? Fullname, int Experience, string Email, string Description, string PhoneNumber);