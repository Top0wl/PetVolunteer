using CSharpFunctionalExtensions;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record TypeDetails
{
    public SpeciesId SpeciesId { get; }
    public BreedId BreedId { get; }
    
    private TypeDetails(SpeciesId speciesId, BreedId breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }

    public static Result<TypeDetails> Create(SpeciesId speciesId, BreedId breedId)
    {
        if(speciesId is null)
            throw new ArgumentNullException(nameof(speciesId));

        if(breedId is null)
            throw new ArgumentNullException(nameof(breedId));

        return new TypeDetails(speciesId, breedId);
    }
    
}