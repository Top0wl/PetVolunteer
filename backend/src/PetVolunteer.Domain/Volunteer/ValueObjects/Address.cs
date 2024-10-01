using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.Volunteer.ValueObjects;

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

    public static Result<Address> Create(string city, string street, string numberHouse)
    {
        if (string.IsNullOrWhiteSpace(city))
            return Result.Failure<Address>($"City is required.");
        if (string.IsNullOrWhiteSpace(street))
            return Result.Failure<Address>($"Street is required.");
        
        return new Address(city, street, numberHouse);
    }
}