using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.Authorization.DataModels;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Authorization.Commands.Login;

public class LoginHandler : ICommandHandler<string, LoginCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenProvider _tokenProvider;
    private readonly ILogger<LoginHandler> _logger;
    
    public LoginHandler(
        UserManager<User> userManager, 
        ITokenProvider tokenProvider, 
        ILogger<LoginHandler> logger)
    {
        _userManager = userManager;
        _tokenProvider = tokenProvider;
        _logger = logger;
    }
    
    public async Task<Result<string, ErrorList>> Handle(
        LoginCommand command, 
        CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user is null)
        {
            return Errors.General.NotFound().ToErrorList();
        }
        
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, command.Password);
        if (passwordConfirmed == false)
        {
            return Errors.User.InvalidCredentials().ToErrorList();
        }
        
        var token = _tokenProvider.GenerateAccessToken(user);
        _logger.LogInformation("Successfully logged in.");

        return token;
    }
}