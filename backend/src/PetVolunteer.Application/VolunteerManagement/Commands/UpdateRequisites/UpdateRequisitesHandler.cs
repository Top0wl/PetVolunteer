using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateRequisites;

public class UpdateRequisitesHandler : ICommandHandler<Guid, UpdateRequisitesCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateRequisitesHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateRequisitesCommand command,
        CancellationToken cancellationToken = default)
    {
        //Получаем волонтёра
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        var volunteer = volunteerResult.Value;

        //Собираем из request'a реквизиты
        var requisites = command.Requisites
            .Select(requisite => Requisite
                .Create(requisite.Title, requisite.Description)
                .Value)
            .ToList();
        
        volunteer.UpdateRequisites(requisites);

        var result = await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Update volunteer (requisites), id: {request.VolunteerId}", command.VolunteerId);

        return result;
    }
}