using FluentValidation;
using PetVolunteer.Application.DTOs.Validators;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.VolunteerManagement.Commands.UploadFilesToPet;

public class UploadFilesToPetCommandValidator : AbstractValidator<UploadFilesToPetCommand>
{
    public UploadFilesToPetCommandValidator()
    {
        RuleFor(u => u.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.PetId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleForEach(u => u.Files).SetValidator(new UploadFileDtoValidator());
    }
}