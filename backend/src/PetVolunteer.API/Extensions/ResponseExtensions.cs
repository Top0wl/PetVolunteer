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
        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        var responseError = new ResponseError(error.Code, error.Message, null);

        var envelope = Envelope.Error([responseError]);

        return new ObjectResult(envelope)
        {
            StatusCode = statusCode,
        };
    }

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

    // public static ActionResult ToResponse(this UnitResult<Error> result)
    // {
    //     if (result.IsSuccess)
    //         return new OkResult();
    //
    //     var statusCode = result.Error.Type switch
    //     {
    //         ErrorType.Validation => StatusCodes.Status400BadRequest,
    //         ErrorType.NotFound => StatusCodes.Status404NotFound,
    //         ErrorType.Conflict => StatusCodes.Status409Conflict,
    //         ErrorType.Failure => StatusCodes.Status500InternalServerError,
    //         _ => StatusCodes.Status500InternalServerError,
    //     };
    //
    //     var envelope = Envelope.Error(result.Error);
    //
    //     return new ObjectResult(envelope)
    //     {
    //         StatusCode = statusCode,
    //     };
    // }
}