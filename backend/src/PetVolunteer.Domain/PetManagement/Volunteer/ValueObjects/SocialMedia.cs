using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record SocialMedia
{
    public string Title { get; } = default!;
    public string Url { get; } = default!;

    private SocialMedia(string title, string url)
    {
        Title = title;
        Url = url;
    }

    public static Result<SocialMedia, Error> Create(string title, string url)
    {
        if (string.IsNullOrEmpty(title))
            return Errors.General.ValueIsRequired(nameof(title));

        if (string.IsNullOrEmpty(url))
            return Errors.General.ValueIsRequired(nameof(url));

        return new SocialMedia(title, url);
    }
}