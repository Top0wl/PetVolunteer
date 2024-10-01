using CSharpFunctionalExtensions;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Species.Entities;

public class Breed : Shared.Entity<BreedId>
{
    public string Name { get; }
    
    private Breed(BreedId id) : base(id) { }

    public Breed(BreedId id, string name) : base(id)
    {
        this.Name = name;
    }

    public static Result<Breed> Create(BreedId id, string name)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Failure<Breed>("Breed is not empty");

        return new Breed(id, name);
    }
}