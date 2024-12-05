using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.VolunteerManagement.Commands;

namespace PetVolunteer.Application.Authorization.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand;