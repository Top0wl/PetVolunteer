using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateSocialMedia;

public class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
{
    public UpdateSocialMediaCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();
        
        RuleForEach(r => r.SocialMedia)
            .MustBeValueObject(r => 
                SocialMedia.Create(r.Title, r.Url));
    }
}