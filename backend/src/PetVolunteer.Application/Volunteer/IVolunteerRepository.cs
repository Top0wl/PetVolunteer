using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer;

public interface IVolunteerRepository
{
    Task<Guid> Add(
        Domain.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Guid> Save(
        Domain.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Guid> Delete(
        Domain.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Result<Domain.Volunteer.Entities.Volunteer, Error>> GetById(
        Guid id, 
        CancellationToken cancellationToken = default);
    Task<Result<Domain.Volunteer.Entities.Volunteer, Error>> GetByEmail(Email email);
}