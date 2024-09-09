using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Enums;

namespace PetVolunteer.Domain.Models;

public class Pet
{
    #region Private Fields
    
    private readonly List<Requisite> _requisites = [];
    
    #endregion Private Fields
    
    #region Public Fields
    public Guid Id { get; private set; }
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
    public IReadOnlyList<Requisite> Requisites => _requisites;
    #endregion Public Fields

    #region Ctor

    private Pet()
    {
    }

    private Pet(Guid id,
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
        Id = id;
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
        Guid id,
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

        var pet = new Pet(id, ownerPhoneNumber, name, petType, description, breed, color, healthInformation, address,
            petStatus, isCastrated, isVaccinated, weight, height, birthDate, createdDate);
        return Result.Success(pet);
    }
}