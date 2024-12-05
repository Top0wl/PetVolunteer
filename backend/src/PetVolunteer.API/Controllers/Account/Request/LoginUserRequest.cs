using PetVolunteer.Application.Authorization.Commands.Login;

namespace PetVolunteer.API.Controllers.Account.Request;

public record LoginUserRequest(string Email, string Password)
{
    public LoginCommand ToCommand() => new (Email, Password);
}