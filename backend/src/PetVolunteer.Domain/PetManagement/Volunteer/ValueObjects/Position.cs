using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public class Position : ValueObject
{
    public int Value { get; }

    private Position(int value)
    {
        Value = value;
    }

    public static Result<Position, Error> Create(int value)
    {
        if (value < 1) 
            return Errors.General.ValueIsInvalid("Serial Number");
        
        return new Position(value);
    }
    
    public Result<Position, Error> Forward()
    {
        return Create(Value + 1);
    }
    
    public Result<Position, Error> Back()
    {
        return Create(Value - 1);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
    
    public static implicit operator int(Position position) => position.Value;
}