using FluentValidation;

namespace PetVolunteer.Application.VolunteerManagement.Commands.Detele;

public class DeleteVolunteerCommandValidator : AbstractValidator<DeleteVolunteerCommand>
{
    public DeleteVolunteerCommandValidator()
    {
        RuleFor(d=> d.VolunteerId).NotEmpty();
    }
}