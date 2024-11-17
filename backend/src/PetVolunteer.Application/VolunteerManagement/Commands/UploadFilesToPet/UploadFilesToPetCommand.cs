using PetVolunteer.Application.DTOs;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UploadFilesToPet;

public record UploadFilesToPetCommand(Guid VolunteerId, Guid PetId, IEnumerable<UploadFileDto> Files);