using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Volunteer.UpdateSocialMedia;

public class UpdateSocialMediaHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UpdateSocialMediaHandler> _logger;

    public UpdateSocialMediaHandler(IVolunteerRepository volunteerRepository, ILogger<UpdateSocialMediaHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        UpdateSocialMediaCommand command,
        CancellationToken cancellationToken = default)
    {
        //Получаем волонтёра
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        var volunteer = volunteerResult.Value;

        //Собираем из request'a соц сети
        var socialMedia = SocialMediaList
            .Create(command.SocialMedia
                .Select(requisite => SocialMedia
                    .Create(requisite.Title, requisite.Url).Value));

        volunteer.UpdateSocialMedia(socialMedia);

        var result = await _volunteerRepository.Save(volunteer, cancellationToken);

        _logger.LogInformation("Update volunteer (requisites), id: {request.VolunteerId}", command.VolunteerId);

        return result;
        
        
    }
}