using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record Requisite
{
    public string Title { get; } = default!;
    public string Description { get; } = default!;
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public static Result<Requisite> Create(string title, string description)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Failure<Requisite>($"Title is required.");
        
        if (string.IsNullOrEmpty(description))
            return Result.Failure<Requisite>($"Description is required.");
        
        var requisite = new Requisite(title, description);
        
        return Result.Success(requisite);
    }
}