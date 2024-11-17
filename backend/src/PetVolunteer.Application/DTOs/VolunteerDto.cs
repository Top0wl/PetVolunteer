namespace PetVolunteer.Application.DTOs;

public class VolunteerDto
{
    //TODO
    public Guid Id { get; init; }
    
    public string Firstname { get; init; } = string.Empty;

    public string Lastname { get; init; } = string.Empty;

    public PetDto[] Pets { get; init; } = [];
}