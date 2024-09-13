namespace PetVolunteer.Domain.ValueObjects.ValueObjectId;

public abstract record ValueObjectId<T>
{
    public Guid Value { get; }
    
    protected  ValueObjectId(Guid guid)
    {
        Value = guid;
    }
    
    public static T NewId() => (T)Activator.CreateInstance(typeof(T), Guid.NewGuid())!;
    public static T Empty() => (T)Activator.CreateInstance(typeof(T), Guid.Empty)!;
    public static T Create(Guid id) => (T)Activator.CreateInstance(typeof(T), id)!;
}

//TODO: Пока, что не вижу смысла выносить Id в отдельный классы, как появится необходимость, вынесу...
public record PetId(Guid Value) : ValueObjectId<PetId>(Value);
public record VolunteerId(Guid Value) : ValueObjectId<VolunteerId>(Value);

