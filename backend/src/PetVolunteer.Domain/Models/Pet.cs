using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Enums;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Models;

public class Pet : Shared.Entity<PetId>
{
    #region Public Fields
    public string Name { get; private set; } = default!;
    public AnimalType AnimalType { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    //TODO: Когда-то потом по заданиям появится новый класс...???
    public string Breed { get; private set; } = default!;
    //TODO: Подумать как лучше хранить Color? Мб хранить в .net структуре Color. Или мб ValueObject сделать, который будет хранить много чего
    public string Color { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public PhoneNumber OwnerPhoneNumber { get; private set; } = default!;
    public PetStatus PetStatus { get; private set; } = default!;
    public HealthInformation HealthInformation { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public PetPhotos? Photos { get; private set; } = default!;
    public RequisitesList? Requisites { get; private set; } = default!;
    
    #endregion Public Fields

    #region Ctor

    private Pet(PetId id) : base(id) { }

    private Pet(PetId id,
        string name,
        AnimalType animalType,
        string description,
        string breed,
        string color,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus petStatus,
        HealthInformation healthInformation,
        DateTime birthDate,
        DateTime createdDate) 
        : base(id)
    {
        Name = name;
        AnimalType = animalType;
        Description = description;
        Breed = breed;
        Color = color;
        Address = address;
        PetStatus = petStatus;
        HealthInformation = healthInformation;
        BirthDate = birthDate;
        CreatedDate = createdDate;
        OwnerPhoneNumber = ownerPhoneNumber;
    }

    #endregion

    public static Result<Pet> Create(
        PetId id,
        string name,
        AnimalType animalType,
        string description,
        string breed,
        string color,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus petStatus,
        HealthInformation healthInformation,
        DateTime birthDate,
        DateTime createdDate)
    {
        
        if (string.IsNullOrEmpty(name))
            return Result.Failure<Pet>($"Name is required.");
        
        if (string.IsNullOrEmpty(breed))
            return Result.Failure<Pet>($"Breed is required.");
        
        if (string.IsNullOrEmpty(color))
            return Result.Failure<Pet>($"Color is required.");

        var pet = new Pet(
            id, 
            name, 
            animalType, 
            description, 
            breed, 
            color, 
            address,
            ownerPhoneNumber,
            petStatus,
            healthInformation,
            birthDate, 
            createdDate);
        
        return Result.Success(pet);
    }
}