using PetVolunteer.Application.Abstractions;

namespace PetVolunteer.Application.VolunteerManagement.Commands.Detele;

public record DeleteVolunteerCommand(Guid VolunteerId) : ICommand;