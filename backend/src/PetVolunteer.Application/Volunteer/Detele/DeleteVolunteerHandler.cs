using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Volunteer.CreateVolunteer;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Volunteer.Detele;

public class DeleteVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    
    public DeleteVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
         
        var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Delete volunteer, id: {VolunteerId}", command.VolunteerId);
        //Соз
        return result;
    }
}