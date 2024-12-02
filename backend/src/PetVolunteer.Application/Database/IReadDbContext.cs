using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.Database;

public interface IReadDbContext
{
    public IQueryable<VolunteerDto> Volunteers { get; }
    public IQueryable<PetDto> Pets { get; }
}