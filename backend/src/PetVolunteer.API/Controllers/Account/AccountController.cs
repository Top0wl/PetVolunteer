using Microsoft.AspNetCore.Mvc;
using PetVolunteer.API.Controllers.Account.Request;
using PetVolunteer.API.Extensions;
using PetVolunteer.Application.Authorization.Commands.Login;
using PetVolunteer.Application.Authorization.Commands.Register;
using PetVolunteer.Infrastructure.Authentication;

namespace PetVolunteer.API.Controllers.Account;

public class AccountController : ApplicationController
{
    [Permission("volunteer.create")]
    [HttpPost("create")]
    public IActionResult CreateVolunteer()
    {
        return Ok();
    }

    [Permission("volunteer.update")]
    [HttpPost("update")]
    public IActionResult UpdateVolunteer()
    {
        return Ok();
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
     }
}