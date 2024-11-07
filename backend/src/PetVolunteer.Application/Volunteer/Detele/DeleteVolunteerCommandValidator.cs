using FluentValidation;

namespace PetVolunteer.Application.Volunteer.Detele;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(d=> d.VolunteerId).NotEmpty();
    }
}