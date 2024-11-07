using CSharpFunctionalExtensions;
using FluentValidation;
using PetVolunteer.Domain.Shared;

namespace PetVolunteer.Application.Validation;

public static class CustomValidatators
{
    //By Vladimir Khorikov
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject, Error>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            Result<TValueObject, Error> result = factoryMethod(value);

            if (result.IsSuccess)
                return;

            context.AddFailure(result.Error.Serialize());
        });
    }
}