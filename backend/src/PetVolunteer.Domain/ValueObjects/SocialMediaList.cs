using PetVolunteer.Domain.Models;

namespace PetVolunteer.Domain.ValueObjects;

public record SocialMediaList
{
    public IReadOnlyList<SocialMedia> SocialMedias {  get; } = default!;
    
    private SocialMediaList() { }
    
    private SocialMediaList(IEnumerable<SocialMedia> list) => SocialMedias = list.ToList();
    
    public static SocialMediaList Create(IEnumerable<SocialMedia> list) => new(list);
}