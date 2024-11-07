using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Species.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Species.Entities;

public class Species : Shared.Entity<SpeciesId>
{
    private readonly List<Breed> _breeds = [];
    public IReadOnlyList<Breed> Breeds => _breeds;
    public TypeAnimal TypeAnimal { get; private set; }
    
    private Species(SpeciesId id) : base(id) { }

    public Species(SpeciesId id, TypeAnimal typeAnimal, IEnumerable<Breed> breeds) : base(id)
    {
        this.TypeAnimal = typeAnimal;
        this._breeds = breeds.ToList();
    }

    public static Result<Species> Create(SpeciesId id, TypeAnimal typeAnimal, IEnumerable<Breed> breeds)
    {
        return new Species(id, typeAnimal, breeds);
    }
}