using CSharpFunctionalExtensions;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.PetManagement.Volunteer.Entities;

public class Pet : Shared.Entity<PetId>, ISoftDeletable
{
    private bool _isDeleted = false;
    
    #region Public Fields
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    //TODO: Подумать как лучше хранить Color? Мб хранить в .net структуре Color. Или мб ValueObject сделать, который будет хранить много чего
    public string Color { get; private set; } = default!;
    public Address Address { get; private set; } = default!;
    public PhoneNumber OwnerPhoneNumber { get; private set; } = default!;
    public PetStatus PetStatus { get; private set; } = default!;
    public HealthInformation HealthInformation { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public ValueObjectList<PetPhoto> Photos { get; private set; }
    public RequisitesList? Requisites { get; private set; }
    public TypeDetails TypeDetails { get; private set; }
    
    #endregion Public Fields

    #region Ctor

    private Pet(PetId id) : base(id) { }

    public Pet(PetId id,
        string name,
        string description,
        string color,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus petStatus,
        HealthInformation healthInformation,
        DateTime birthDate,
        DateTime createdDate, 
        ValueObjectList<PetPhoto> photos,
        TypeDetails typeDetails)
        : base(id)
    {
        Name = name;
        Description = description;
        Color = color;
        Address = address;
        PetStatus = petStatus;
        HealthInformation = healthInformation;
        BirthDate = birthDate;
        CreatedDate = createdDate;
        OwnerPhoneNumber = ownerPhoneNumber;
        Photos = photos;
        TypeDetails = typeDetails;
    }

    #endregion

    public static Result<Pet> Create(
        PetId id,
        string name,
        string description,
        string color,
        Address address,
        PhoneNumber ownerPhoneNumber,
        PetStatus petStatus,
        HealthInformation healthInformation,
        DateTime birthDate,
        DateTime createdDate,
        ValueObjectList<PetPhoto> photos,
        TypeDetails typeDetails)
    {
        if (string.IsNullOrEmpty(name))
            return Result.Failure<Pet>($"Name is required.");
        
        
        if (string.IsNullOrEmpty(color))
            return Result.Failure<Pet>($"Color is required.");

        var pet = new Pet(
            id, 
            name, 
            description, 
            color, 
            address,
            ownerPhoneNumber,
            petStatus,
            healthInformation,
            birthDate, 
            createdDate,
            photos,
            typeDetails);
        
        return Result.Success(pet);
    }

    public void Delete()
    {
        if (_isDeleted == false)
            _isDeleted = true;
    }
    
    public void Restore()
    {
        if (_isDeleted)
            _isDeleted = false;
    }
}