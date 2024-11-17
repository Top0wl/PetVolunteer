using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.Database;
using PetVolunteer.Infrastructure;
using PetVolunteer.Infrastructure.DbContexts;

namespace PetVolunteer.API.Extensions;

public static class AppExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}