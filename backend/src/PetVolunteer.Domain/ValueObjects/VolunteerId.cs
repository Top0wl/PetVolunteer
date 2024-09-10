namespace PetVolunteer.Domain.ValueObjects;

public record VolunteerId
{
    public Guid Value { get; }
    
    private VolunteerId(Guid guid)
    {
        Value = guid;
    }

    public VolunteerId NewPetId() => new(Guid.NewGuid());

    public VolunteerId Empty() => new(Guid.Empty);
}