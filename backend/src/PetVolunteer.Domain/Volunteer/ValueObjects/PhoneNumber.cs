using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.Volunteer.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex ValidatePhoneNumberRegex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
    public string Value { get; }
    
    private PhoneNumber(string value)
    {
        Value = value;
    }
    
    public static Result<PhoneNumber, Error> Create(string phoneNumber)
    {
        if (!ValidatePhoneNumberRegex.IsMatch(phoneNumber))
            return Errors.General.ValueIsInvalid(nameof(phoneNumber));
        
        return new PhoneNumber(phoneNumber);
    }
}