namespace PetVolunteer.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValueIsInvalid(string? name = null)
        {
            var label = name ?? "Value";
            return Error.Validation($"{label}.is.invalid", $"{label} is invalid");
        }
        
        public static Error NotFound(Guid? id = null)
        {
            var forId = id == null ? "" : $" for Id '{id}'";
            return Error.Validation($"record.not.found", $"Record not found{forId}");
        }
        
        public static Error ValueIsRequired(string? name = null)
        {
            var label = name == null ? "" : " " + name;
            return Error.Validation($"length.is.invalid", $"Invalid{label} length");
        }
    }

    public static class Volunteer
    {
        public static Error EmailIsAlreadyExist()
        {
           return Error.Validation($"email.already.exist", $"Email already exist");
        }
    }
}