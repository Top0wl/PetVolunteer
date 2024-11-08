using PetVolunteer.Application.DTOs;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;

namespace PetVolunteer.Application.Volunteer.AddPet;

public record AddPetCommand
{
    public Guid VolunteerId { get; set; }
    public Guid SpeciesId { get; set; }
    public Guid BreedId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public AddressDto Address { get; set; }
    public string PhoneOwner { get; set; }
    public PetStatus PetStatus { get; set; }
    public HealthInformationDto HealthInformation { get; set; }
    public DateTime BirthDate { get; set; }
    public IEnumerable<CreateFileDto> Files { get; set; }
    
    public AddPetCommand(Guid volunteerId, Guid speciesId, Guid breedId, string name, string description, string color, AddressDto address, string phoneOwner, PetStatus petStatus, HealthInformationDto healthInformation, DateTime birthDate, IEnumerable<CreateFileDto> files)
    {
        VolunteerId = volunteerId;
        SpeciesId = speciesId;
        BreedId = breedId;
        Name = name;
        Description = description;
        Color = color;
        Address = address;
        PhoneOwner = phoneOwner;
        PetStatus = petStatus;
        HealthInformation = healthInformation;
        BirthDate = birthDate;
        Files = files;
    }
}

public record CreateFileDto(Stream Stream, string FileName);