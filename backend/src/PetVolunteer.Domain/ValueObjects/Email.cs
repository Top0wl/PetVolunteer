using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record Email
{
    private static readonly Regex ValidationRegex = new(@"^[\w-\.]{1,40}@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Singleline | RegexOptions.Compiled);
    public string Value { get; }

    private Email() { }
    private Email(string value)
    {
        Value = value;
    }
    
    public static Result<Email> Create(string email)
    {
        if (!ValidationRegex.IsMatch(email))
            return Result.Failure<Email>($"Email is not valid.");

        return new Email(email);
    }
}