using CSharpFunctionalExtensions;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;
using PetVolunteer.Domain.Volunteer.Enums;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Domain.Volunteer.Entities;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    #region Private Fields
    
    private readonly List<Pet> _pets = [];

    private bool _isDeleted = false;
    
    #endregion Private Fields

    #region Public Fields
    
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public ExperienceWork Experience { get; private set; } = default!;
    public RequisitesList? Requisites { get; private set; }
    public SocialMediaList? SocialMediaList { get; private set; }
    public IReadOnlyList<Pet> Pets => _pets;

    #endregion Public Fields

    #region Ctor

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    public Volunteer(
        VolunteerId id, 
        FullName fullName, 
        Email email, 
        string description, 
        PhoneNumber phoneNumber, 
        ExperienceWork experience)
        : base (id)
    {
        FullName = fullName;
        Email = email;
        Description = description;
        PhoneNumber = phoneNumber;
        Experience = experience;
    }

    #endregion Ctor

    public int CountOfFoundHome => CountOfPetStatus(PetStatus.FoundHome);
    public int CountOfNeedHelp => CountOfPetStatus(PetStatus.NeedHelp);
    public int CountOfLookingForHome => CountOfPetStatus(PetStatus.LookingForHome);

    public int CountOfPetStatus(PetStatus petStatus)
    {
        return Pets.Count(p => p.PetStatus == petStatus);
    }

    public static Result<Volunteer> Create(
        VolunteerId id,
        FullName fullname,
        Email email,
        string description,
        PhoneNumber phoneNumber,
        ExperienceWork experience)
    {
        var volunteer = new Volunteer(
            id, 
            fullname,
            email, 
            description, 
            phoneNumber, 
            experience);
        
        return Result.Success(volunteer);
    }

    public void UpdateMainInfo(string? description, PhoneNumber phoneNumber)
    {
        Description = description!;
        PhoneNumber = phoneNumber;
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