using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerCommandValidator()
    {
        RuleFor(r => r.FullName).MustBeValueObject(fn => FullName.Create(fn.LastName, fn.FirstName, fn.Patronymic));
        RuleFor(r => r.PhoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(r => r.Email).MustBeValueObject(Email.Create);
    }
}