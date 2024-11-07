using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
{
    public UpdateMainInfoCommandValidator()
    {
        RuleFor(r => r.VolunteerId).NotNull().NotEmpty();
        
        RuleFor(r => r.Email).MustBeValueObject(Email.Create);
    }
}