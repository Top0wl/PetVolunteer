using PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

namespace PetVolunteer.API.Controllers.Volunteer.Requests;

public record GetVolunteersWithPaginationRequest(
    int? CountPets,
    int? WorkExperience,
    int Page, 
    int PageSize)
{
    public GetVolunteersWithPaginationQuery ToQuery()
    {
        
        return new GetVolunteersWithPaginationQuery(CountPets, WorkExperience, Page, PageSize);
    }
}