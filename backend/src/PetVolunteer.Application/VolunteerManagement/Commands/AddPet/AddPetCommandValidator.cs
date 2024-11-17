using FluentValidation;

namespace PetVolunteer.Application.VolunteerManagement.Commands.AddPet;

public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        
    }
}