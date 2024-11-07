using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record PetPhoto
{
    public FilePath PathToStorage { get; }
    public bool IsMain { get; } 
    
    public PetPhoto(FilePath pathToStorage, bool isMain)
    {
        PathToStorage = pathToStorage;
        IsMain = isMain;
    }
}