using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.CreateVolunteer;

public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(r => r.FullName).MustBeValueObject(fn => FullName.Create(fn.LastName, fn.FirstName, fn.Patronymic));
        RuleFor(r => r.phoneNumber).MustBeValueObject(PhoneNumber.Create);
        RuleFor(r => r.email).MustBeValueObject(Email.Create);
    }
}