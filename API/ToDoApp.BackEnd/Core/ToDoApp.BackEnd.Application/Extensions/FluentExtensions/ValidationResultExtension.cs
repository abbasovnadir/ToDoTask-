using ToDoApp.BackEnd.Application.Objects.Concrete;
using FluentValidation.Results;

namespace ToDoApp.BackEnd.Application.Extensions.FluentExtensions;
public static class ValidationResultExtension
{
    public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult result)
    {
        List<CustomValidationError> errors = new();

        foreach (var error in result.Errors)
        {
            errors.Add(new()
            {
                ErrorMessage = error.ErrorMessage,
                PropertyName = error.PropertyName,
            });
        }
        return errors;
    }
}
