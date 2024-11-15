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
    private readonly Mock<IFileProvider> _fileProviderMock = new();
    private readonly Mock<IVolunteerRepository> _volunteerRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<UploadFilesToPetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<UploadFilesToPetHandler>> _loggerMock = new();
    private readonly CancellationToken _cancellationToken = new CancellationTokenSource().Token;


    [Fact]
    public async Task Handle_ShouldUploadFilesToPet()
    {
        //Arrange
        var volunteer = CreateVolunteerWithPets(1);
        var stream = new MemoryStream();
        var fileName = "test.jpg";
        var uploadFile = new UploadFileDto(stream, fileName);
        List<UploadFileDto> uploadFiles = [uploadFile, uploadFile];
        var command = new UploadFilesToPetCommand(volunteer.Id, volunteer.Pets.First().Id, uploadFiles);

        List<FilePath> filePaths =
        [
            FilePath.Create(fileName).Value,
            FilePath.Create(fileName).Value,
        ];
        _fileProviderMock
            .Setup(v => v.UploadAsync(It.IsAny<List<FileUploadInfo>>(), _cancellationToken))
            .ReturnsAsync(Result.Success<IReadOnlyList<FilePath>, Error>(filePaths));

        _volunteerRepositoryMock
            .Setup(v => v.GetById(volunteer.Id, _cancellationToken))
            .ReturnsAsync(volunteer);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(_cancellationToken))
            .Returns(Task.CompletedTask);

        _validatorMock
            .Setup(v => v.ValidateAsync(command, _cancellationToken))
            .ReturnsAsync(new ValidationResult());

        var handler = new UploadFilesToPetHandler(
            _fileProviderMock.Object,
            _volunteerRepositoryMock.Object,
            _loggerMock.Object,
            _validatorMock.Object,
            _unitOfWorkMock.Object);

        //Act
        var result = await handler.Handle(command, _cancellationToken);

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

        var volunteer =
            new Domain.PetManagement.Volunteer.Entities.Volunteer(volunteerId, fullname, email, description, phone,
                experienceWork);

        for (int i = 0; i < petsCount; i++)
        {
            var pet = CreatePet(i + 1);
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