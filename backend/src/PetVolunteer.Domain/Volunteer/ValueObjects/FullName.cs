using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.Volunteer.ValueObjects;

public record FullName
{
    public string LastName { get; } = default!;
    public string FirstName { get; } = default!;
    public string? Patronymic { get; } = default!;
    
    private FullName(string lastName, string firstName, string patronymic)
    {
        LastName = lastName;
        FirstName = firstName;
        Patronymic = patronymic;
    }

    public static Result<FullName, Error> Create(string lastName, string firstName, string patronymic)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Errors.General.ValueIsRequired(nameof(lastName));
        
        if(string.IsNullOrWhiteSpace(firstName))
            return Errors.General.ValueIsRequired(nameof(firstName));

        return new FullName(lastName, firstName, patronymic);
    }
}