namespace PetVolunteer.Domain.ValueObjects.ValueObjectId;

public record VolunteerId(Guid Value) : ValueObjectId<VolunteerId>(Value);