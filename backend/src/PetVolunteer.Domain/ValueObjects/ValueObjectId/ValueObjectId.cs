namespace PetVolunteer.Domain.ValueObjects.ValueObjectId;

public abstract record ValueObjectId<T> where T : ValueObjectId<T>
{
    public Guid Value { get; }
    
    protected ValueObjectId(Guid guid)
    {
        Value = guid;
    }
    
    public static T NewId() => (T)Activator.CreateInstance(typeof(T), Guid.NewGuid())!;
    public static T Empty() => (T)Activator.CreateInstance(typeof(T), Guid.Empty)!;
    public static T Create(Guid id) => (T)Activator.CreateInstance(typeof(T), id)!;
    
    public static implicit operator Guid(ValueObjectId<T> id) => id.Value;
    public static implicit operator T(ValueObjectId<T> generic) => Create(generic.Value);
    public static implicit operator ValueObjectId<T>(Guid guid) => Create(guid);
    
    public static T FromGuid(Guid guid) => Create(guid);
}

