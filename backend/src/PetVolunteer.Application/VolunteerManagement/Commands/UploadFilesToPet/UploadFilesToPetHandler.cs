using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetVolunteer.Application.Abstractions;
using PetVolunteer.Application.Database;
using PetVolunteer.Application.Extensions;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UploadFilesToPet;

public class UploadFilesToPetHandler : ICommandHandler<Guid, UploadFilesToPetCommand>
{
    private const string BUCKET_NAME = "photos";
    
    private readonly IFileProvider _fileProvider;
    private readonly IVolunteerRepository _volunteerRepository;
    private readonly ILogger<UploadFilesToPetHandler> _logger;
    private readonly IValidator<UploadFilesToPetCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    //private readonly IMessageQueue<IEnumerable<(FilePath, string)>> _messageQueue;
    

    public UploadFilesToPetHandler(
        IFileProvider fileProvider, 
        IVolunteerRepository volunteerRepository, 
        ILogger<UploadFilesToPetHandler> logger, 
        IValidator<UploadFilesToPetCommand> validator, 
        IUnitOfWork unitOfWork)
        //IMessageQueue<Imerable<(FilePath, string)>> messageQueuEnue)
    {
        _fileProvider = fileProvider;
        _volunteerRepository = volunteerRepository;
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
        //_messageQueue = messageQueue;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UploadFilesToPetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return validationResult.ToErrorList();
        }
        
        var volunteerResult = await _volunteerRepository.GetById(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        List<FileUploadInfo> listFileUploadInfo = [];
        foreach (var file in command.Files)
        {
            //TODO: Точно ли нужно называть FilePath VO. Всё таки в minio это не путь, а название файла по сути...
            var objectName = FilePath.Create(file.FileName);
            if(objectName.IsFailure)
                return objectName.Error.ToErrorList();
             
            var fileUploadInfo = new FileUploadInfo(file.Stream, objectName.Value, BUCKET_NAME);
            listFileUploadInfo.Add(fileUploadInfo);
        }
        
        var uploadResult = await _fileProvider.UploadAsync(listFileUploadInfo, cancellationToken);
        if (uploadResult.IsFailure)
        {
            //await _messageQueue.WriteAsync(
            //    listFileUploadInfo.Select(f => (f.ObjectName, f.BucketName)), 
            //    cancellationToken);
            
            return uploadResult.Error.ToErrorList();
        }

        var petPhotos = uploadResult.Value
            .Select(f=> new PetPhoto(f, false))
            .ToList();
        
        petResult.Value.UpdateFiles(petPhotos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Success uploaded files to pet - {id}", petResult.Value.Id.Value);

        return petResult.Value.Id.Value;
    }
}