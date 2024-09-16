using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record ExperienceWork
{
    public int Value { get; }
    private ExperienceWork(int value)
    {
        Value = value;
    }
    
    public static Result<ExperienceWork> Create(int experienceWork)
    {
        if (experienceWork > 0)
            return Result.Failure<ExperienceWork>($"Experience can't been < 0.");

        return new ExperienceWork(experienceWork);
    }
}
