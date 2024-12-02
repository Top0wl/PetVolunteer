using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateSocialMedia;

public class UpdateSocialMediaHandler : ICommandHandler<Guid, UpdateSocialMediaCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialMediaHandler> _logger;

    public UpdateSocialMediaHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateSocialMediaHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdateSocialMediaCommand command,
        CancellationToken cancellationToken = default)
    {
        //Получаем волонтёра
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        var volunteer = volunteerResult.Value;

        //Собираем из request'a соц сети
        var socialMedia = command.SocialMedia
            .Select(requisite => SocialMedia
                .Create(requisite.Title, requisite.Url)
                .Value)
            .ToList();

        volunteer.UpdateSocialMedia(socialMedia);

        var result = await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Update volunteer (requisites), id: {request.VolunteerId}", command.VolunteerId);

        return result;
        
        
    }
}