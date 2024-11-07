namespace PetVolunteer.Application.DTOs;

public record HealthInformationDto
{
    public double Weight { get; set; }
    public double Height { get; set; }
    public bool IsCastrated { get; set; }
    public bool IsVaccinated { get; set; }
    public string AdditionalHealthInformation { get; set; }
}