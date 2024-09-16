using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain.ValueObjects;

public record HealthInformation
{
    public double Weight { get; }
    public double Height { get; }
    public bool IsCastrated { get; }
    public bool IsVaccinated { get; }
    public string AdditionalHealthInformation { get; }
    
    private HealthInformation(double weight, double height, bool isCastrated, bool isVaccinated, string additionalHealthInformation)
    {
        Weight = weight;
        Height = height;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        AdditionalHealthInformation = additionalHealthInformation;
    }

    public static Result<HealthInformation> Create(double weight, double height, bool isCastrated, bool isVaccinated, string additionalHealthInformation)
    {
        if (weight <= 0)
            return Result.Failure<HealthInformation>($"Weight is not to be < 0");
        
        if (height <= 0)
            return Result.Failure<HealthInformation>($"Height is not to be < 0");
        
        return new HealthInformation(weight, height, isCastrated, isVaccinated, additionalHealthInformation);
    }
}