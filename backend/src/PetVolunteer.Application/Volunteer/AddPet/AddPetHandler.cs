using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Database;
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
    private readonly IApplicationDbContext _dbContext;


    public AddPetHandler(
        IFileProvider fileProvider, 
        IVolunteerRepository volunteerRepository, 
        ILogger<AddPetHandler> logger,
        IApplicationDbContext dbContext)
    {
        _fileProvider = fileProvider;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Result<Guid, Error>> Handle(
        AddPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

        try
        {
            //Получаем волонтёра
            var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
            if (volunteerResult.IsFailure)
                return volunteerResult.Error;
            var petId = Domain.ValueObjects.ValueObjectId.PetId.NewId();
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
            
            List<FileUploadInfo> listFileUploadInfo = [];
            foreach (var file in command.Files)
            {
                //TODO: Точно ли нужно называть FilePath VO. Всё таки в minio это не путь, а название файла по сути...
                var objectName = FilePath.Create(file.FileName);
                if(objectName.IsFailure)
                    return objectName.Error;
             
                var fileUploadInfo = new FileUploadInfo(file.Stream, objectName.Value, BUCKET_NAME);
                listFileUploadInfo.Add(fileUploadInfo);
            }
            
            var petPhotos = listFileUploadInfo
                .Select(f=> f.ObjectName)
                .Select(f=> new PetPhoto(f, false))
                .ToList();
            
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
                petPhotos,
                typeDetails);
            
            volunteerResult.Value.AddPet(pet);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var uploadResult = await _fileProvider.UploadAsync(listFileUploadInfo, cancellationToken);
            if(uploadResult.IsFailure)
                return uploadResult.Error;
            
            await transaction.CommitAsync(cancellationToken);
            
            return pet.Id.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Can not add pet to volunteer - {id} in transaction", command.VolunteerId);
            await transaction.RollbackAsync(cancellationToken);
            return Error.Failure("volunteer.pet.failure", "Can not add pet to volunteer - {id}");
            
        }
        
    }
}