using PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        return new GetVolunteersWithPaginationQuery(Page, PageSize);
    }
}