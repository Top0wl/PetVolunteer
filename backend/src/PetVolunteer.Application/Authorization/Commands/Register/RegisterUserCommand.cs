using PetVolunteer.Application.Abstractions;

namespace PetVolunteer.Application.Authorization.Commands.Register;

public record RegisterUserCommand(string Email, string UserName, string Password) : ICommand;