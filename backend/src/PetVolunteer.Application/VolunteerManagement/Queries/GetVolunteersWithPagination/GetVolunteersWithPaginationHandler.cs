using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Extensions;
using PetVolunteer.Application.Models;

namespace PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }
    
    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query, CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers.AsQueryable();
        
        // Будущая фильтрация и сортировка
        
        return await volunteerQuery.ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }
}