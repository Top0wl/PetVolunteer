using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record SocialMedia
{
    public string Title { get; } = default!;
    public string Url { get; } = default!;
    private SocialMedia(string title, string url)
    {
        Title = title;
        Url = url;
    }
    public static Result<SocialMedia> Create(string title, string url)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Failure<SocialMedia>($"Title is required.");
        
        if (string.IsNullOrEmpty(url))
            return Result.Failure<SocialMedia>($"Url is required.");
        
        var socialMedia = new SocialMedia(title, url);
        
        return Result.Success(socialMedia);
    }
}