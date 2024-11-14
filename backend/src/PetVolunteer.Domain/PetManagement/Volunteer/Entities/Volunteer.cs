using CSharpFunctionalExtensions;
using PetVolunteer.Domain.PetManagement.Volunteer.Enums;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;
using PetVolunteer.Domain.Shared;
using PetVolunteer.Domain.ValueObjects.ValueObjectId;

namespace PetVolunteer.Domain.PetManagement.Volunteer.Entities;

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
    public RequisitesList? RequisitesList { get; private set; }
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
        : base(id)
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

    public void UpdateRequisites(RequisitesList requisitesList)
    {
        RequisitesList = requisitesList;
    }

    public void UpdateSocialMedia(SocialMediaList socialMediaList)
    {
        SocialMediaList = socialMediaList;
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

    public UnitResult<Error> AddPet(Pet pet)
    {
        var serialNumberResult = Position.Create(_pets.Count + 1);
        if (serialNumberResult.IsFailure)
            return serialNumberResult.Error;
        pet.SetPosition(serialNumberResult.Value);

        //валидация + логика
        _pets.Add(pet);
        return Result.Success<Error>();
    }

    public Result<Pet, Error> GetPetById(Guid petId)
    {
        var pet = Pets.FirstOrDefault(p => p.Id == petId);
        if (pet is null)
            return Errors.General.NotFound(petId);

        return pet;
    }

    public UnitResult<Error> MovePet(Pet pet, Position newPosition)
    {
        var currentPosition = pet.Position;

        if (currentPosition == newPosition || _pets.Count == 1)
            return Result.Success<Error>();

        var adjustedPosition = AdjustNewPositionIfOutOfRange(newPosition);
        if (adjustedPosition.IsFailure)
            return adjustedPosition.Error;

        newPosition = adjustedPosition.Value;

        var moveResult = MovePetsBetweenPositions(newPosition, currentPosition);
        if (moveResult.IsFailure)
            moveResult = moveResult.Error;
        
        pet.SetPosition(newPosition);

        return Result.Success<Error>();
    }

    private UnitResult<Error> MovePetsBetweenPositions(Position newPosition, Position currentPosition)
    {
        if (newPosition.Value < currentPosition.Value)
        {
            var petsToMove = _pets.Where(p => p.Position.Value >= newPosition.Value
                                              && p.Position.Value < currentPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveForeward();
                if (result.IsFailure)
                    return result.Error;
            }
        }
        else if (newPosition.Value > currentPosition.Value)
        {
            var petsToMove = _pets
                .Where(p => p.Position.Value > currentPosition.Value
                            && p.Position.Value <= newPosition.Value);

            foreach (var petToMove in petsToMove)
            {
                var result = petToMove.MoveBack();
                if (result.IsFailure)
                    return result.Error;
            }
        }

        return Result.Success<Error>();
    }

    private Result<Position, Error> AdjustNewPositionIfOutOfRange(Position newPosition)
    {
        if (newPosition.Value <= _pets.Count)
            return newPosition;

        var lastPosition = Position.Create(_pets.Count - 1);
        if (lastPosition.IsFailure)
            return lastPosition.Error;

        return lastPosition.Value;
    }
}