using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Species.Entities;
using PetVolunteer.Domain.Species.ValueObjects;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Species;

public class Species : Shared.Entity<SpeciesId>
{
    public List<Breed>? Breeds { get; }
    public TypeAnimal TypeAnimal { get; }

    private Species(SpeciesId id) : base(id) { }

    public Species(SpeciesId id, TypeAnimal typeAnimal, List<Breed>? breeds) : base(id)
    {
        this.TypeAnimal = typeAnimal;
        this.Breeds = breeds;
    }

    public static Result<Species> Create(SpeciesId id, TypeAnimal typeAnimal, List<Breed> breeds)
    {
        return new Species(id, typeAnimal, breeds);
    }
}