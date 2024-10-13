using FluentValidation;
using PetVolunteer.API.DTOs;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.Volunteer.ValueObjects;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoDtoValidator()
    {
        RuleFor(r => r.PhoneNumber).MustBeValueObject(PhoneNumber.Create)
            .When(r => !string.IsNullOrWhiteSpace(r.PhoneNumber));
    }
}