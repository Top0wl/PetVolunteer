using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PetVolunteer.API.Response;
using PetVolunteer.Domain.Shared;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace PetVolunteer.API.Validation;
/*
public class ustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails == null)
            throw new InvalidOperationException("ValidationProblemDetails is null");

        List<ResponseError> errors = [];
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var responseErrors = from errorMessage in validationErrors
                let error = Error.Deserialize(errorMessage)
                select new ResponseError(error.Code, error.Message, invalidField);
            errors.AddRange(responseErrors);
        }

        var envelope = Envelope.Error(errors);
        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest
        };
    }
}
*/