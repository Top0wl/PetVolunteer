using CSharpFunctionalExtensions;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PetVolunteer.Application.Providers.FileProvider;
using PetVolunteer.Application.Volunteer;
using PetVolunteer.Application.Volunteer.AddPet;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Application.UnitTests;

public class UploadFilesToPetTests
{
    [Fact]
    public async Task Handle_ShouldUploadFilesToPet()
    {
        //Arrange
        var volunteer = CreateVolunteerWithPets(1);
        
        var cancellationToken = new CancellationTokenSource().Token;
        var stream = new MemoryStream();
        var fileName = "test.jpg";
        var uploadFile = new UploadFileDto(stream, fileName);
        List<UploadFileDto> uploadFiles = [uploadFile, uploadFile];
        var command = new UploadFilesToPetCommand(volunteer.Id, volunteer.Pets.First().Id, uploadFiles);
        
        var fileProviderMock = new Mock<IFileProvider>();
        List<FilePath> filePaths =
        [
            FilePath.Create(fileName).Value,
            FilePath.Create(fileName).Value,
        ];
        fileProviderMock
            .Setup(v => v.UploadAsync(It.IsAny<List<FileUploadInfo>>(), cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));

        var volunteerRepositoryMock = new Mock<IVolunteerRepository>();
        volunteerRepositoryMock
            .Setup(v => v.GetById(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);
        
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(cancellationToken))
            .Returns(Task.CompletedTask);
        
        var validatorMock = new Mock<IValidator<UploadFilesToPetCommand>>();
        validatorMock
            .Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());
        
        //var Logger = LoggerFactory.Create()
        
        var loggerMock = new Mock<ILogger<UploadFilesToPetHandler>>();
        //loggerMock.Setup(l => l.LogInformation("Success"));
        
        var handler = new UploadFilesToPetHandler(
            fileProviderMock.Object,
            volunteerRepositoryMock.Object,
            loggerMock.Object,
            validatorMock.Object,
            unitOfWorkMock.Object);
        
        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        volunteer.Pets.First().Photos.Should().HaveCount(2);
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(volunteer.Pets.First().Id);
        //result
    }
    
    private Domain.PetManagement.Volunteer.Entities.Volunteer CreateVolunteerWithPets(int petsCount)
    {
        var volunteerId = VolunteerId.NewId();
        var fullname = FullName.Create("LastName", "FirstName", "Partonymic").Value;
        var email = Email.Create("mail@mail.ru").Value;
        var experienceWork = ExperienceWork.Create(1).Value;
        var phone = PhoneNumber.Create("+7-777-777-7777").Value;
        var description = "str";
        
        var volunteer = new Domain.PetManagement.Volunteer.Entities.Volunteer(volunteerId, fullname, email, description, phone, experienceWork);

        for (int i = 0; i < petsCount; i++)
        {
            var pet = CreatePet(i+1);
            volunteer.AddPet(pet);
        }

        return volunteer;
    }
    
    private Pet CreatePet(int? nameIdentifier = null)
    {
        var petId = PetId.NewId();
        var name = "pet" + nameIdentifier;
        var description = "description";
        var color = "white";
        var address = Address.Create("city", "street", "1").Value;
        var phone = PhoneNumber.Create("+7-777-7777").Value;
        var petStatus = PetStatus.NeedHelp;
        var health = HealthInformation.Create(1, 1, true, true, description).Value;
        var date = DateTime.Now;
        ValueObjectList<PetPhoto> photos = [];
        var typeDetails = TypeDetails.Create(SpeciesId.NewId(), BreedId.NewId()).Value;
        var pet = new Pet(petId, name, description, color, address, phone,
            petStatus, health, date, date, [], typeDetails);

        return pet;
    }
}


