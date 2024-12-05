using PetVolunteer.Application.Authorization.Commands.Register;

namespace PetVolunteer.API.Controllers.Account.Request;

public record RegisterUserRequest(string Email, string UserName, string Password)
{
    public RegisterUserCommand ToCommand() => new (Email, UserName, Password);
};