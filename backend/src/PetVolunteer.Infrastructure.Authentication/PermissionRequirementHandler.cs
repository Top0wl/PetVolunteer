using Microsoft.AspNetCore.Authorization;

namespace PetVolunteer.Infrastructure.Authentication;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionAttribute>
{
    public PermissionRequirementHandler()
    {
        
    }
    
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAttribute permission)
    {
        var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.Sub)!.Value;
    }
}