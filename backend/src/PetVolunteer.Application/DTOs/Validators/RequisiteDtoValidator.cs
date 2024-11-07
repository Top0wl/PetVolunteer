using FluentValidation;

namespace PetVolunteer.Application.DTOs.Validators;

public class RequisiteDtoValidator : AbstractValidator<RequisiteDto>
{
    public RequisiteDtoValidator()
    {
        RuleFor(r => r.Description).NotEmpty();
        RuleFor(r => r.Title).NotEmpty();
    }
}