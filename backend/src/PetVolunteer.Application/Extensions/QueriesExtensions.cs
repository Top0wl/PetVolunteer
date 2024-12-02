using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.Models;

namespace PetVolunteer.Application.Extensions;

public static class QueriesExtensions
{
    public static async Task<PagedList<T>> ToPagedListAsync<T>(
        this IQueryable<T> source, 
        int page, 
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        page = page == 0 ? 1 : page;
        pageSize = pageSize == 0 ? 100 : page;
        
        var totalCount = await source.CountAsync(cancellationToken); 
        
        var items = await source
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        return new PagedList<T>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
    
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source, 
        bool condition,
        Expression<Func<T,bool>> predicate)
    {
        return condition ? source.Where(predicate) : source;
    }
}