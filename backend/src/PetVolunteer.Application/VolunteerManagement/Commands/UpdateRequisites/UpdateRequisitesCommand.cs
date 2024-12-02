using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateRequisites;

public record UpdateRequisitesCommand(Guid VolunteerId, IEnumerable<RequisiteDto> Requisites) : ICommand;