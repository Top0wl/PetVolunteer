using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record Address
{
    public string City { get; }
    public string Street { get; }
    public string NumberHouse { get; }
    
    private Address(string city, string street, string numberHouse)
    {
        City = city;
        Street = street;
        NumberHouse = numberHouse;
    }

    public static Result<Address, Error> Create(string city, string street, string numberHouse)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Errors.General.ValueIsRequired(nameof(city));
        
        if (string.IsNullOrWhiteSpace(street))
            return Errors.General.ValueIsRequired(nameof(street));
        
        return new Address(city, street, numberHouse);
    }
}