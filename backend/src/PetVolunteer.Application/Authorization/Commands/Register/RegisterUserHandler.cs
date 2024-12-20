using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.Authorization.DataModels;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Authorization.Commands.Register;

public class RegisterUserHandler : ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ILogger<RegisterUserHandler> _logger;
    
    public RegisterUserHandler(UserManager<User> userManager, ILogger<RegisterUserHandler> logger)
    {
        _userManager = userManager;
        _logger = logger;
    }
    
    public async Task<UnitResult<ErrorList>> Handle(
        RegisterUserCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user = new User()
        {
            Email = command.Email,
            UserName = command.UserName,
        };
        
        var result = await _userManager.CreateAsync(user, command.Password);
        if (result.Succeeded)
        {
            _logger.LogInformation("User created: {userName} a new account with password.", command.UserName);
            return Result.Success<ErrorList>();
        }
        
        var errors = result.Errors
            .Select(e => Error.Failure(e.Code, e.Description))
            .ToList();
        
        return new ErrorList(errors);
    }
}