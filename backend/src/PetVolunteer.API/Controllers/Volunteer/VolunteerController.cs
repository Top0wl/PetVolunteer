using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetVolunteer.API.Contracts;
using PetVolunteer.API.Controllers.Requests;
using PetVolunteer.API.Extensions;
using PetVolunteer.API.Processors;
using PetVolunteer.API.Response;
using PetVolunteer.Application.DTOs;
using PetVolunteer.Application.Volunteer.AddPet;
using PetVolunteer.Application.Volunteer.CreateVolunteer;
using PetVolunteer.Application.Volunteer.Detele;
using PetVolunteer.Application.Volunteer.UpdateMainInfo;
using PetVolunteer.Application.Volunteer.UpdateRequisites;
using PetVolunteer.Application.Volunteer.UpdateSocialMedia;

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
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }

    [HttpPut("{volunteerId:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid volunteerId,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request.ToCommand(volunteerId), cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpDelete("{volunteerId:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid volunteerId,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteVolunteerCommand(volunteerId);
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPut("{volunteerId:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid volunteerId,
        [FromServices] UpdateRequisitesHandler handler,
        [FromBody] UpdateRequisitesRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId);
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPut("{volunteerId:guid}/social-media")]
    public async Task<ActionResult<Guid>> UpdateSocialMedia(
        [FromRoute] Guid volunteerId,
        [FromServices] UpdateSocialMediaHandler handler,
        [FromBody] UpdateSocialMediaRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId); 
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpPost("{volunteerId:guid}/pet")]
    public async Task<ActionResult<Guid>> AddPet(
        [FromRoute] Guid volunteerId,
        [FromBody] AddPetRequest request,
        [FromServices] AddPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(volunteerId);
        
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
    
    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/files")]
    public async Task<ActionResult<Guid>> UploadFilesToPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] IFormFileCollection files,
        [FromServices] UploadFilesToPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();
        var fileDtos = fileProcessor.Process(files);
        var command = new UploadFilesToPetCommand(volunteerId, petId, fileDtos);
        
        var result = await handler.Handle(command, cancellationToken);
        if(result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}