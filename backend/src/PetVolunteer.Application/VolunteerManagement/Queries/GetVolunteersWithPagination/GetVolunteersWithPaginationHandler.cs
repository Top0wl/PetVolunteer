using System.Text.Json;
using Dapper;
using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Extensions;
using PetVolunteer.Application.Models;

namespace PetVolunteer.Application.VolunteerManagement.Queries.GetVolunteersWithPagination;

public class GetVolunteersWithPaginationHandler
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly IReadDbContext _readDbContext;

    public GetVolunteersWithPaginationHandler(IReadDbContext readDbContext)
    {
        _readDbContext = readDbContext;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var volunteerQuery = _readDbContext.Volunteers;

        volunteerQuery = volunteerQuery.WhereIf(
            query.CountPets != null,
            v => v.Pets.Length >= query.CountPets);

        volunteerQuery = volunteerQuery.WhereIf(
            query.WorkExperience != null,
            v => v.Experience >= query.WorkExperience);

        return await volunteerQuery.OrderBy(v => v.Id)
            .ToPagedListAsync(query.Page, query.PageSize, cancellationToken);
    }
}

public class GetVolunteersWithPaginationHandlerDapper
    : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetVolunteersWithPaginationHandlerDapper(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<PagedList<VolunteerDto>> Handle(GetVolunteersWithPaginationQuery query,
        CancellationToken cancellationToken)
    {
        var connection = _sqlConnectionFactory.Create();

        var parameters = new DynamicParameters();

        var totalCount = await connection
            .ExecuteScalarAsync<int>("select count(*) from volunteers");
        
        var sql = """
                  select 
                      v.Id,
                      v.FirstName,
                      v.LastName,
                      v.Patronymic,
                      v.Description,
                      v.Email,
                      v.Experience,
                      v.phone_number,
                      v.requisites
                  from volunteers v
                  order by v.Id limit @PageSize offset @Offset 
                  """;

        if (query.CountPets != null)
            sql += " where ";
        
        parameters.Add("@PageSize", query.PageSize);
        parameters.Add("@Offset", (query.Page - 1) * query.PageSize);
        
        var volunteers = await connection.QueryAsync<VolunteerDto, RequisiteDto[], VolunteerDto>(
            sql,
            (volunteer, requisitesJson) =>
            {
                volunteer.Requisites = requisitesJson;
                return volunteer;
            },
            splitOn: "requisites",
            param: parameters);

        return new PagedList<VolunteerDto>()
        {
            Items = volunteers.ToList(),
            TotalCount = totalCount,
            PageSize = query.PageSize,
            Page = query.Page,
        };
    }
}