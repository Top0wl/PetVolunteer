using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.Species.ValueObjects;

public record TypeAnimal
{
    public string Name { get; }

    private TypeAnimal(string name)
    {
        this.Name = name;
    }

    public static Result<TypeAnimal> Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return Result.Failure<TypeAnimal>("Type Animal not can be empty");
        
        return new TypeAnimal(value);
    }
}