using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.PetManagement.Volunteer.ValueObjects;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UpdateRequisites;

public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty();

        RuleForEach(r => r.Requisites)
            .MustBeValueObject(r =>
                Requisite.Create(r.Title, r.Description));
            
        //.OverridePropertyName("Requisites[{CollectionIndex}].SomeProperty");
    }
}