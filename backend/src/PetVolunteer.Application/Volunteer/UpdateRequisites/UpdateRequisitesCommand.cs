using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Volunteer.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites);