using Microsoft.AspNetCore.Mvc;
using PetVolunteer.API.Response;

namespace PetVolunteer.API.Controllers;

public abstract class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);
        
        return base.Ok(envelope);
    }
}