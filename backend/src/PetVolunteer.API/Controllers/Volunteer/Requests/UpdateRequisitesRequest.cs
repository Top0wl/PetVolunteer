using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.VolunteerManagement.Commands.UpdateRequisites;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId)
    {
        return new UpdateRequisitesCommand(volunteerId, Requisites);
    }
}