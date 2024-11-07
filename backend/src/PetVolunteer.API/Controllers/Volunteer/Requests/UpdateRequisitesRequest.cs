using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Volunteer.UpdateRequisites;

namespace PetVolunteer.API.Controllers.Requests;

public record UpdateRequisitesRequest(IEnumerable<RequisiteDto> Requisites)
{
    public UpdateRequisitesCommand ToCommand(Guid volunteerId)
    {
        return new UpdateRequisitesCommand(volunteerId, Requisites);
    }
}