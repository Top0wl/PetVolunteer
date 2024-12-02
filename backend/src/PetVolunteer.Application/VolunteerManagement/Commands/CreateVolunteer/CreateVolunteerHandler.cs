using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Application.VolunteerManagement.Commands.CreateVolunteer;

public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
{
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    
    public CreateVolunteerHandler(IVolunteerRepository volunteerRepository, ILogger<CreateVolunteerHandler> logger)
    {
        _volunteerRepository = volunteerRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreateVolunteerCommand command,
        CancellationToken cancellationToken = default)
    {
        //Создания доменной модели
        var volunteerId = VolunteerId.NewId();
        var fullName = FullName.Create(command.FullName.LastName, command.FullName.FirstName, command.FullName.Patronymic).Value;
        var email = Email.Create(command.Email).Value;
        var phone = PhoneNumber.Create(command.PhoneNumber).Value;
        var experienceOfWork = ExperienceWork.Create(command.ExperienceOfWork).Value;
        
        var existVolunteer = await _volunteerRepository.GetByEmail(email);
        if(existVolunteer.IsSuccess)
            return Errors.Volunteer.EmailIsAlreadyExist().ToErrorList();
        
        var volunteer = new Domain.PetManagement.Volunteer.Entities.Volunteer(
            volunteerId, 
            fullName, 
            email, 
            command.Description,
            phone,
            experienceOfWork);
        
        //Сохранение в базу
        await _volunteerRepository.Add(volunteer, cancellationToken);
        
        _logger.LogInformation("Created volunteer: {volunteerId}", volunteerId);
        
        return (Guid)volunteerId;
    }
}