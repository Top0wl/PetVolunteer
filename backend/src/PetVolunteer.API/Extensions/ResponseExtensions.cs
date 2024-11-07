using CSharpFunctionalExtensions;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PetVolunteer.API.Response;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var statusCode = GetStatusCodeForErrorType(error.Type);
        
        var envelope = Envelope.Error(error);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode,
        };
    }
    
    public static ActionResult ToResponse(this ErrorList errors)
    {
        if (!errors.Any())
            return new ObjectResult(Envelope.Error(errors))
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };
        
        var distinctErrorTypes = errors
            .Select(x => x.Type)
            .Distinct()
            .ToList();
        
        var statusCode = distinctErrorTypes.Count() > 1 
            ? StatusCodes.Status500InternalServerError 
            : GetStatusCodeForErrorType(distinctErrorTypes.First());
        
        var envelope = Envelope.Error(errors);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode,
        };
    }

    private static int GetStatusCodeForErrorType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };
    
    /*
    public static ActionResult ToValidationErrorResponse(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            throw new InvalidOperationException("Result can't be created");

        var validationErrors = validationResult.Errors;
        var responceErrors = from validationError in validationErrors
            let errorMessage = validationError.ErrorMessage
            let error = Error.Deserialize(validationError.ErrorCode)
            select new ResponseError(error.Code, error.Message, validationError.PropertyName);

        var envelope = Envelope.Error(responceErrors);
        return new ObjectResult(envelope)
        {
            StatusCode = StatusCodes.Status400BadRequest,
        };
    }
    */
}