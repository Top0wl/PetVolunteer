namespace PetVolunteer.Domain.ValueObjects.ValueObjectId;

public record PetId(Guid Value) : ValueObjectId<PetId>(Value);