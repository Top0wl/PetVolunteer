using CSharpFunctionalExtensions;

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

    public static Result<FullName> Create(string lastName, string firstName, string patronymic)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            return Result.Failure<FullName>($"LastName is required.");
        if (string.IsNullOrWhiteSpace(firstName))
            return Result.Failure<FullName>($"FirstName is required.");

        return new FullName(lastName, firstName, patronymic);
    }
}