namespace PetVolunteer.Application.DTOs;

public class VolunteerDto
{
    public Guid Id { get; set; }
    
    public string Firstname { get; set; } = string.Empty;

    public string Lastname { get; set; } = string.Empty;
    
    public string Patronymic { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public int Experience { get; set; } = 0;
    
    public string PhoneNumber { get; set; } = string.Empty;
    
    public RequisiteDto[] Requisites { get; set; }
    
    public SocialMediaDto[]? SocialMedia { get; set; }
    
    public PetDto[] Pets { get; set; } = [];
}