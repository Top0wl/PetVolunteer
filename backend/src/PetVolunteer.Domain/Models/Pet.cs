using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Enums;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Models;

public class Pet : Shared.Entity<PetId>
{
    #region Private Fields

    
    
    #endregion Private Fields
    
    #region Public Fields
    public string Name { get; private set; } = default!;
    public string PetType { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Breed { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInformation { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string OwnerPhoneNumber { get; private set; } = default!;
    public PetStatus PetStatus { get; private set; } = default!;
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public PetPhotos? Photos { get; private set; } = default!;
    public RequisitesList? Requisites { get; private set; } = default!;
    
    #endregion Public Fields

    #region Ctor

    private Pet(PetId id) : base(id)
    {
    }

    private Pet(PetId id,
        string name,
        string petType,
        string description,
        string breed,
        string color,
        string healthInformation,
        string address,
        string ownerPhoneNumber,
        PetStatus petStatus,
        bool isCastrated,
        bool isVaccinated,
        double weight,
        double height,
        DateTime birthDate,
        DateTime createdDate) 
        : base(id)
    {
        Name = name;
        PetType = petType;
        Description = description;
        Breed = breed;
        Color = color;
        HealthInformation = healthInformation;
        Address = address;
        PetStatus = petStatus;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
        CreatedDate = createdDate;
        OwnerPhoneNumber = ownerPhoneNumber;
    }

    #endregion

    public static Result<Pet> Create(
        PetId id,
        string name,
        string petType,
        string description,
        string breed,
        string color,
        string healthInformation,
        string address,
        string ownerPhoneNumber,
        PetStatus petStatus,
        bool isCastrated,
        bool isVaccinated,
        double weight,
        double height,
        DateTime birthDate,
        DateTime createdDate)
    {
        
        if (string.IsNullOrEmpty(name))
            return Result.Failure<Pet>($"Name is required.");
        
        if (string.IsNullOrEmpty(petType))
            return Result.Failure<Pet>($"PetType is required.");
        
        if (string.IsNullOrEmpty(breed))
            return Result.Failure<Pet>($"Breed is required.");
        
        if (string.IsNullOrEmpty(color))
            return Result.Failure<Pet>($"Color is required.");
        
        if (string.IsNullOrEmpty(color))
            return Result.Failure<Pet>($"Owner phone number is required.");

        if (weight <= 0)
            return Result.Failure<Pet>($"Weight is not to be < 0");
        
        if (height <= 0)
            return Result.Failure<Pet>($"Height is not to be < 0");

        var pet = new Pet(
            id, 
            name, 
            petType, 
            description, 
            breed, 
            color, 
            healthInformation, 
            address,
            ownerPhoneNumber,
            petStatus, 
            isCastrated, 
            isVaccinated, 
            weight, 
            height, 
            birthDate, 
            createdDate);
        
        return Result.Success(pet);
    }
}