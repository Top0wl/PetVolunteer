using PetVolunteer.API.DTOs;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public record UpdateMainInfoRequest(Guid VolunteerId, UpdateMainInfoDto Dto);