namespace PetVolunteer.Domain.ValueObjects;

public record PetId
{
    public Guid Value { get; }
    
    private PetId(Guid guid)
    {
        Value = guid;
    }

    public PetId NewPetId() => new(Guid.NewGuid());
    
    public PetId Empty() => new(Guid.Empty);
}