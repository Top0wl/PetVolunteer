using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.Volunteer.ValueObjects;

public record Email
{
    private static readonly Regex ValidationRegex = new(@"^[\w-\.]{1,40}@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Singleline | RegexOptions.Compiled);
    public string Value { get; }
    
    private Email(string value)
    {
        Value = value;
    }
    
    public static Result<Email, Error> Create(string email)
    {
        if (!ValidationRegex.IsMatch(email))
            return Errors.General.ValueIsInvalid(nameof(email));

        return new Email(email);
    }
}