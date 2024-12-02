using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Abstractions;

public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken = default);
}