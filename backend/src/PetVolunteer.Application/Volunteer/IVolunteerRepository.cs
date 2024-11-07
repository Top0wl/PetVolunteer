using CSharpFunctionalExtensions;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Volunteer;

public interface IVolunteerRepository
{
    Task<Guid> Add(
        Domain.PetManagement.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Guid> Save(
        Domain.PetManagement.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Guid> Delete(
        Domain.PetManagement.Volunteer.Entities.Volunteer volunteer, 
        CancellationToken cancellationToken = default);
    Task<Result<Domain.PetManagement.Volunteer.Entities.Volunteer, Error>> GetById(
        Guid id, 
        CancellationToken cancellationToken = default);
    Task<Result<Domain.PetManagement.Volunteer.Entities.Volunteer, Error>> GetByEmail(Email email);
}