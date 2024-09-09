using CSharpFunctionalExtensions;

namespace PetVolunteer.Domain;

public class Pet
{
    private readonly List<Requisite> _requisites = [];

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string PetType { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string Breed { get; private set; } = default!;
    public string Color { get; private set; } = default!;
    public string HealthInformation { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public string OwnerPhoneNumber { get; private set; } = default!;
    public string HelpStatus { get; private set; } = default!;
    public bool IsCastrated { get; private set; }
    public bool IsVaccinated { get; private set; }
    public double Weight { get; private set; }
    public double Height { get; private set; }
    public DateTime BirthDate { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public IReadOnlyList<Requisite> Requisites => _requisites;

    private Pet() { }

    private Pet(string ownerPhoneNumber, string name, string petType, string description, string breed, string color,
        string healthInformation, string address, string helpStatus, bool isCastrated, bool isVaccinated, double weight,
        double height, DateTime birthDate, DateTime createdDate)
    {
        Name = name;
        PetType = petType;
        Description = description;
        Breed = breed;
        Color = color;
        HealthInformation = healthInformation;
        Address = address;
        HelpStatus = helpStatus;
        IsCastrated = isCastrated;
        IsVaccinated = isVaccinated;
        Weight = weight;
        Height = height;
        BirthDate = birthDate;
        CreatedDate = createdDate;
        OwnerPhoneNumber = ownerPhoneNumber;
    }

    public static Pet Create(
        string ownerPhoneNumber,
        string name,
        string petType,
        string description,
        string breed,
        string color,
        string healthInformation,
        string address,
        string helpStatus, 
        bool isCastrated, 
        bool isVaccinated,
        double weight, 
        double height,
        DateTime birthDate,
        DateTime createdDate)
    {
        return new Pet(
            ownerPhoneNumber, 
            name, 
            petType, 
            description, 
            breed, 
            color, 
            healthInformation, 
            address,
            helpStatus, 
            isCastrated, 
            isVaccinated, 
            weight, 
            height, 
            birthDate, 
            createdDate);
    }
}