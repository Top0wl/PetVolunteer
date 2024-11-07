using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

public record ExperienceWork
{
    public int Value { get; }
    private ExperienceWork(int value)
    {
        Value = value;
    }
    
    public static Result<ExperienceWork, Error> Create(int experienceWork)
    {
        if (experienceWork < 0)
            return Errors.General.ValueIsInvalid(nameof(experienceWork));

        return new ExperienceWork(experienceWork);
    }
}
