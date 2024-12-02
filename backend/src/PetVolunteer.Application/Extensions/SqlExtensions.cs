using System.Text;
using Dapper;

namespace PetVolunteer.Application.Extensions;

public static class SqlExtensions
{
    public static void ApplyPagination(
        this StringBuilder sqlBuilder, 
        DynamicParameters parameters,
        int page,
        int pageSize)
    {
        parameters.Add("@PageSize", page);
        parameters.Add("@Offset", (page - 1) * pageSize);
        
        sqlBuilder.Append(" limit @PageSize offset @Offset");
    }
    
    public static void ApplySorting(
        this StringBuilder sqlBuilder, 
        DynamicParameters parameters,
        string sortBy,
        string sortDirection)
    {
        parameters.Add("@SortBy", sortBy);
        parameters.Add("@SortDirection", sortDirection);
        
        sqlBuilder.Append(" order by @SortBy @SortDirection");
    }
}