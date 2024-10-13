using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Shared;

public abstract class Entity<TId> where TId : ValueObjectId<TId>
{
    public TId Id { get; private set; }

    protected Entity(TId id) => Id = id;
}