using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.VolunteerManagement.Commands.Detele;

public class DeleteVolunteerHandler : ICommandHandler<Guid, DeleteVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<DeleteVolunteerHandler> _logger;
    
    public DeleteVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<DeleteVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeleteVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
         
        var result = await _volunteerRepository.Delete(volunteerResult.Value, cancellationToken);
        
        _logger.LogInformation("Delete volunteer, id: {VolunteerId}", command.VolunteerId);
        //Соз
        return result;
    }
}