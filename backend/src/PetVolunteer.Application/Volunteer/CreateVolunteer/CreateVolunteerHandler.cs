using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using PetVolunteer.Domain.Volunteer.Entities;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerHandler
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    
    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> Handle(
        CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        //Создания доменной модели
        var volunteerId = VolunteerId.NewId();
        var fullName = FullName.Create(request.FullName.LastName, request.FullName.FirstName, request.FullName.Patronymic).Value;
        var email = Email.Create(request.email).Value;
        var phone = PhoneNumber.Create(request.phoneNumber).Value;
        var experienceOfWork = ExperienceWork.Create(request.experienceOfWork).Value;
        
        var existVolunteer = await _volunteerRepository.GetByEmail(email);
        if(existVolunteer.IsSuccess)
            return Errors.Volunteer.EmailIsAlreadyExist();
        
        var volunteer = new Domain.Volunteer.Entities.Volunteer(
            volunteerId, 
            fullName, 
            email, 
            request.description,
            phone,
            experienceOfWork);
        
        //Сохранение в базу
        await _volunteerRepository.Add(volunteer, cancellationToken);
        
        _logger.LogInformation("Created volunteer: {volunteerId}", volunteerId);
        
        return (Guid)volunteerId;
    }
}