using FluentValidation;

namespace PetVolunteer.Application.Volunteer.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        
    }
}