using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record PetPhoto
{
    public string Path { get; private set; } = default!;
    public bool IsMain { get; private set; } = default!;

    private PetPhoto(string path, bool isMain)
    {
        Path = path;
        IsMain = isMain;
    }

    public static Result<PetPhoto> Create(string path, bool isMain)
    {
        if (string.IsNullOrWhiteSpace(path))
            return Result.Failure<PetPhoto>("Path cannot be empty");

        return new PetPhoto(path, isMain);
    }
}