using System.Security.Principal;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Extensions;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly IValidator<UpdateMainInfoCommand> _validator;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(
        IVolunteerRepository volunteerRepository,
        IValidator<UpdateMainInfoCommand> validator,
        ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _validator = validator;
        _logger = logger; 
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateMainInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;
        var description = command.Description;
        
        volunteerResult.Value.UpdateMainInfo(description, phoneNumber);
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Update volunteer, id: {request.VolunteerId}", command.VolunteerId);

        return result;
    }
}