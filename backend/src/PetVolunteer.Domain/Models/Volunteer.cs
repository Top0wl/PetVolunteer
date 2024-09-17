using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Enums;
using PetVolunteer.Domain.ValueObjects;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.Models;

public class Volunteer : Shared.Entity<VolunteerId>
{
    #region Private Fields
    
    private readonly List<Pet> _pets = [];

    #endregion Private Fields

    #region Public Fields
    
    public FullName FullName { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public PhoneNumber PhoneNumber { get; private set; } = default!;
    public ExperienceWork Experience { get; private set; } = default!;
    public RequisitesList Requisites { get; private set; } = default!;
    public SocialMediaList SocialMediaList { get; private set; } = default!;
    public IReadOnlyList<Pet> Pets => _pets;

    #endregion Public Fields

    #region Ctor

    private Volunteer(VolunteerId id) : base(id)
    {
    }

    private Volunteer(
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
}