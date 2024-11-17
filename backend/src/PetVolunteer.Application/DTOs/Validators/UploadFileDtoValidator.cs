using FluentValidation;
using PetVolunteer.Application.Validation;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.DTOs.Validators;

public class UploadFileDtoValidator : AbstractValidator<UploadFileDto>
{
    public UploadFileDtoValidator()
    {
        RuleFor(u => u.FileName).NotEmpty().WithError(Errors.General.ValueIsRequired());
        RuleFor(u => u.Stream).Must(s => s.Length < 5000000);
    }
}