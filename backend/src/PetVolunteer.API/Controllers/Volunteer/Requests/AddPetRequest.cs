using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Volunteer.AddPet;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;

namespace PetVolunteer.API.Contracts;

public record AddPetRequest
{
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

    public AddPetCommand ToCommand(Guid volunteerId)
    {
        var command = new AddPetCommand(
            volunteerId,
            SpeciesId,
            BreedId,
            Name,
            Description,
            Color,
            Address,
            PhoneOwner,
            PetStatus,
            HealthInformation,
            BirthDate);
        return command;
    }
}
