using FluentValidation;

namespace PetVolunteer.Application.Volunteer.Detele;

public class DeleteVolunteerRequestValidator : AbstractValidator<DeleteVolunteerRequest>
{
    public DeleteVolunteerRequestValidator()
    {
        RuleFor(d=> d.VolunteerId).NotEmpty();
    }
}