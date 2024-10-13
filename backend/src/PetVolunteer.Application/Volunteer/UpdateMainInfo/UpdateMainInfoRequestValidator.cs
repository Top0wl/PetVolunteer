using FluentValidation;

namespace PetVolunteer.Application.Volunteer.UpdateMainInfo;

public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotNull().NotEmpty();
    }
}