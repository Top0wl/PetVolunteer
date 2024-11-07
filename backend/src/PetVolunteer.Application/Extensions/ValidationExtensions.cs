using FluentValidation.Results;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Extensions;

public static class ValidationExtensions
{
    public static ErrorList ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors;
            
        var errors = from validationError in validationErrors 
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(errorMessage)
            select Error.Validation(error.Code, error.Message, validationError.PropertyName);

        return errors.ToList();
    }
}