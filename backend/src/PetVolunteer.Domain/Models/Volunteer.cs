using CSharpFunctionalExtensions;
using PetVolunteer.Domain.Enums;
using PetVolunteer.Domain.ValueObjects;

namespace PetVolunteer.Domain.Models;

public class Volunteer
{
    #region Private Fields

    private readonly List<Requisite> _requsites = [];
    private readonly List<SocialMedia> _socialMedia = [];
    private readonly List<Pet> _pets = [];

    #endregion Private Fields

    #region Public Fields

    public VolunteerId Id { get; private set; }
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string Surname { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;
    public int Experience { get; private set; } = default!;
    public IReadOnlyList<Requisite> Requisites => _requsites;
    public IReadOnlyList<SocialMedia> SocialMedia => _socialMedia;
    public IReadOnlyList<Pet> Pets => _pets;

    #endregion Public Fields

    #region Ctor

    private Volunteer()
    {
    }

    private Volunteer(
        VolunteerId id, 
        string firstName, 
        string lastName, 
        string surname, 
        string email, 
        string description, 
        string phoneNumber, 
        int experience)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
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
        string firstName,
        string lastName,
        string surname,
        string email,
        string description,
        string phoneNumber,
        int experience)
    {
        if (string.IsNullOrEmpty(firstName))
            return Result.Failure<Volunteer>($"FirstName is required.");
        
        if (string.IsNullOrEmpty(lastName))
            return Result.Failure<Volunteer>($"LastName is required.");
        
        if (string.IsNullOrEmpty(email))
            return Result.Failure<Volunteer>($"Email is required.");
        
        var volunteer = new Volunteer(
            id, 
            firstName, 
            lastName, 
            surname, 
            email, 
            description, 
            phoneNumber, 
            experience);
        
        return Result.Success(volunteer);
    }
}