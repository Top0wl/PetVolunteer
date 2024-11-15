using System;
using FluentAssertions;
using PetVolunteer.Domain.PetManagement.Volunteer.Entities;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using Xunit;

namespace PetVolunteer.UnitTests;

public class VolunteerTests
{
    [Fact]
    public void AddPet_FirstPet_ReturnSuccessResult()
    {
        //Arrange
        var volunteer = CreateVolunteerWithPets(0);
        var pet = CreatePet();
        
        //Act
        var result = volunteer.AddPet(pet);

        //Assert
        var petResult = volunteer.GetPetById(pet.Id);
        
        result.IsSuccess.Should().BeTrue();
        petResult.IsSuccess.Should().BeTrue();
        petResult.Value.Id.Should().Be(pet.Id);
        petResult.Value.Position.Value.Should().Be(1);
    }
    
    [Fact]
    public void AddPet_AddNewPetToVolunteerWithPets_ReturnSuccessResult()
    {
        //Arrange
        const int countPets = 5;
        var volunteer = CreateVolunteerWithPets(countPets);
        var newPet = CreatePet(countPets + 1);
        
        //Act
        var result = volunteer.AddPet(newPet);

        //Assert
        var petResult = volunteer.GetPetById(newPet.Id);
        
        result.IsSuccess.Should().BeTrue();
        petResult.IsSuccess.Should().BeTrue();
        petResult.Value.Id.Should().Be(newPet.Id);
        petResult.Value.Position.Value.Should().Be(countPets + 1);
    }
    
    [Fact]
    public void MovePet_ShouldNotMove_WhenPetAlreadyAtNewPosition()
    {
        //50
        //Arrange
        const int petsCount = 5;
        var volunteer = CreateVolunteerWithPets(petsCount);
        var secondPosition = Position.Create(2).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(secondPet, secondPosition);

        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        secondPet.Position.Value.Should().Be(2);
        thirdPet.Position.Value.Should().Be(3);
        fourthPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_ShouldMoveOtherPetsForward_WhenNewPositionIsLower()
    {
        //1 2 3 4 5    //4 -> 2
        //1 4 2 3 5
        //1 [4 2 3] 5
        
        //Arrange
        const int petsCount = 5;
        var volunteer = CreateVolunteerWithPets(petsCount);
        var secondPosition = Position.Create(2).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(fourthPet, secondPosition);

        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        fourthPet.Position.Value.Should().Be(2);
        secondPet.Position.Value.Should().Be(3);
        thirdPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    [Fact]
    public void MovePet_ShouldMoveOtherPetsForward_WhenNewPositionIsBigger()
    {
        //1 2 3 4 5   //2 => 4
        //1 3 4 2 5
        //1 [3 4 2] 5
        
        //Arrange
        const int petsCount = 5;
        var volunteer = CreateVolunteerWithPets(petsCount);
        var fourthPosition = Position.Create(4).Value;
        
        var firstPet = volunteer.Pets[0];
        var secondPet = volunteer.Pets[1];
        var thirdPet = volunteer.Pets[2];
        var fourthPet = volunteer.Pets[3];
        var fifthPet = volunteer.Pets[4];
        
        //Act
        var result = volunteer.MovePet(secondPet, fourthPosition);

        //Assert
        result.IsSuccess.Should().BeTrue();
        firstPet.Position.Value.Should().Be(1);
        thirdPet.Position.Value.Should().Be(2);
        fourthPet.Position.Value.Should().Be(3);
        secondPet.Position.Value.Should().Be(4);
        fifthPet.Position.Value.Should().Be(5);
    }
    
    private Volunteer CreateVolunteerWithPets(int petsCount)
    {
        var volunteerId = VolunteerId.NewId();
        var fullname = FullName.Create("LastName", "FirstName", "Partonymic").Value;
        var email = Email.Create("mail@mail.ru").Value;
        var experienceWork = ExperienceWork.Create(1).Value;
        var phone = PhoneNumber.Create("+7-777-777-7777").Value;
        var description = "str";
        
        var volunteer = new Volunteer(volunteerId, fullname, email, description, phone, experienceWork);

        for (int i = 0; i < petsCount; i++)
        {
            var pet = CreatePet(i+1);
            volunteer.AddPet(pet);
        }

        return volunteer;
    }

    //1:22
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