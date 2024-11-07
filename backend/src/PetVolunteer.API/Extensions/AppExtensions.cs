using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.Database;
using PetVolunteer.Infrastructure;

namespace PetVolunteer.API.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}