using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record Requisite
{
    public string Title { get; } = default!;
    public string Description { get; } = default!;
    private Requisite(string title, string description)
    {
        Title = title;
        Description = description;
    }
    public static Result<Requisite, Error> Create(string title, string description)
    {
        if (string.IsNullOrEmpty(title))
            return Errors.General.ValueIsRequired(nameof(title));
        
        if (string.IsNullOrEmpty(description))
            return Errors.General.ValueIsRequired(nameof(description));
        
        return new Requisite(title, description);
    }
}