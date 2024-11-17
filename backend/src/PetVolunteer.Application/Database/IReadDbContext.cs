using Microsoft.EntityFrameworkCore;
using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Database;

public interface IReadDbContext
{
    public DbSet<VolunteerDto> Volunteers { get; }
    public DbSet<PetDto> Pets { get; }
}