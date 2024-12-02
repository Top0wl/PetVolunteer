using PetVolunteer.Application.Abstractions;

namespace PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public record GetVolunteersWithPaginationQuery(
    int? CountPets,
    int? WorkExperience,
    int Page, 
    int PageSize) : IQuery;