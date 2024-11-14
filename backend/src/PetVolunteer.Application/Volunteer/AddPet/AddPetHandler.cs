using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Extensions;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Application.Volunteer.AddPet;

public class AddPetHandler
{
    private const string BUCKET_NAME = "photos";
    
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<AddPetHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPetCommand> _validator;

    public AddPetHandler(
        IFileProvider fileProvider, 
        IVolunteerRepository volunteerRepository, 
        ILogger<AddPetHandler> logger,
        IUnitOfWork unitOfWork, 
        IValidator<AddPetCommand> validator)
    {
        _fileProvider = fileProvider;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        //Получаем волонтёра
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        //Создаём pet'a из команды
        var pet = InitPet(command);

        //Добавляем его к волонтёру
        volunteerResult.Value.AddPet(pet);
        
        //Сохраняем
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return pet.Id.Value;
    }

    private Pet InitPet(AddPetCommand command)
    {
        var petId = PetId.NewId();
        var address = Address.Create(command.Address.City, command.Address.Street, command.Address.NumberHouse).Value;
        var phoneNumber = PhoneNumber.Create(command.PhoneOwner).Value;
        var petStatus = command.PetStatus;
        var healthInformation = HealthInformation.Create(
            command.HealthInformation.Weight,
            command.HealthInformation.Height, 
            command.HealthInformation.IsCastrated, 
            command.HealthInformation.IsVaccinated,
            command.HealthInformation.AdditionalHealthInformation).Value;
        var typeDetails = TypeDetails.Create(
            new SpeciesId(command.SpeciesId), new BreedId(command.BreedId)).Value;
        
        var pet = new Pet(
            petId, 
            command.Name,
            command.Description,
            command.Color,
            address,
            phoneNumber,
            petStatus,
            healthInformation,
            command.BirthDate,
            DateTime.Now,
            [],
            typeDetails);
        return pet;
    }
}