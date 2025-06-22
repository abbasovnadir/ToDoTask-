using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Utilities.ValidationRules.ApplicationValidations.EmailTemplateValidators;
internal sealed class UpdateEmailTemplateCommandValidator : AbstractValidator<UpdateEmailTemplateCommand>
{
    public UpdateEmailTemplateCommandValidator()
    {
        RuleFor(x => x.ID)
            .NotEmpty().WithMessage("ID cannot be empty.")
            .GreaterThan(0).WithMessage("ID must be greater than zero.")
            .InclusiveBetween(1, int.MaxValue).WithMessage("ID must be a valid positive number.");

        RuleFor(x => x.Type)
         .Must(value => Enum.IsDefined(typeof(EmailTemplateTypes), value))
         .WithMessage("Type must be a valid value in EmailTemplateTypes.");

        RuleFor(x => x.TypeName)
        .NotEmpty().WithMessage("TypeName cannot be empty.")
        .MaximumLength(128).WithMessage("TypeName must not exceed 128 characters.");

        RuleFor(x => x.Template)
        .NotEmpty().WithMessage("Template cannot be empty.")
        .MaximumLength(1500).WithMessage("Template must not exceed 1500 characters.");
    }
}
