using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Volunteer.UpdateMainInfo;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Volunteer.UpdateRequisites;

public class UpdateRequisitesHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateRequisitesHandler> _logger;

    public UpdateRequisitesHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateRequisitesHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateRequisitesCommand command,
        CancellationToken cancellationToken = default)
    {
        //Получаем волонтёра
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        var volunteer = volunteerResult.Value;

        //Собираем из request'a реквизиты
        var requisites = RequisitesList
            .Create(command.Requisites
                .Select(requisite => Requisite
                    .Create(requisite.Title, requisite.Description).Value));

        volunteer.UpdateRequisites(requisites);

        var result = await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Update volunteer (requisites), id: {request.VolunteerId}", command.VolunteerId);

        return result;
    }
}