using PetVolunteer.Domain.Models;

namespace PetVolunteer.Domain.ValueObjects;

public record VolunteerSocialMedias
{
    public List<SocialMedia> SocialMedias { get; }
}