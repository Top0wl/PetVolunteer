using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public class UpdateMainInfoHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateMainInfoHandler> _logger;

    public UpdateMainInfoHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateMainInfoHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger; 
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.GetById(request.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;

        var phoneNumber = PhoneNumber.Create(request.Dto.PhoneNumber).Value;
        var description = request.Dto.Description;
        
        volunteerResult.Value.UpdateMainInfo(description, phoneNumber);
        
        var result = await _volunteerRepository.Save(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Update volunteer, id: {request.VolunteerId}", request.VolunteerId);

        return result;
    }
}