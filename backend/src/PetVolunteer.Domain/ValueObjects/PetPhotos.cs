namespace PetVolunteer.Domain.ValueObjects;

public record PetPhotos
{
    public IReadOnlyList<PetPhoto> Photos { get; } = default!;
    
    private PetPhotos() { }
    
    private PetPhotos(IEnumerable<PetPhoto> list) => Photos = list.ToList();
    
    public static PetPhotos Create(IEnumerable<PetPhoto> list) => new(list);
}