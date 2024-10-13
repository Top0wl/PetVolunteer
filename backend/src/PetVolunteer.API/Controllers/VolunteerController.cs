using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetVolunteer.API.DTOs;
using PetVolunteer.API.Extensions;
using PetVolunteer.API.Response;
using PetVolunteer.Application.Volunteer.CreateVolunteer;
using PetVolunteer.Application.Volunteer.Detele;
using PetVolunteer.Application.Volunteer.UpdateMainInfo;

namespace PetVolunteer.API.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteerController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //Вызвать сервис для создания волонтёра (вызов бизнес логики)
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        [FromBody] UpdateMainInfoDto dto,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(id, dto);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }
        
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteVolunteerRequest(id);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToValidationErrorResponse();
        }
        
        var result = await handler.Handle(request, cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
}