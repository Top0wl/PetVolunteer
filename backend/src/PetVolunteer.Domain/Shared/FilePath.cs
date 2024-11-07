using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.Shared;

public record FilePath
{
    public string ObjectName { get; }

    private FilePath(string objectName)
    {
        ObjectName = objectName;
    }

    public static Result<FilePath, Error> Create(string filename)
    {
        var extension = System.IO.Path.GetExtension(filename);
        var objectName = Guid.NewGuid() + extension;
        //Todo: Валидация на доступные расширения
        return new FilePath(objectName);
    }

}