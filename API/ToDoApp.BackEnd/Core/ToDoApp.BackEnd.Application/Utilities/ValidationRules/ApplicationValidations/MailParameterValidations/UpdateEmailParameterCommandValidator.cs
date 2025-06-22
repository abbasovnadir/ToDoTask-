using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Utilities.ValidationRules.ApplicationValidations.MailParameterValidations
{
    public class UpdateEmailParameterCommandValidator : AbstractValidator<UpdateEmailParameterCommand>
    {
        public UpdateEmailParameterCommandValidator()
        {
            RuleFor(x => x.ID)
                .NotEmpty().WithMessage("ID cannot be empty.")
                .GreaterThan(0).WithMessage("ID must be greater than zero.")
                .InclusiveBetween(1, int.MaxValue).WithMessage("ID must be a valid positive number.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .MaximumLength(128).WithMessage("Email must not exceed 128 characters.")
                .EmailAddress().WithMessage("Email must be in a valid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MaximumLength(128).WithMessage("Password must not exceed 128 characters.");

            RuleFor(x => x.SMTP)
                .NotEmpty().WithMessage("SMTP cannot be empty.")
                .MaximumLength(128).WithMessage("SMTP must not exceed 128 characters.");

            RuleFor(x => x.Port)
                .NotEmpty().WithMessage("Port cannot be empty.")
                .GreaterThan(0).WithMessage("Port must be greater than zero.");

            RuleFor(x => x.SSL)
                .Must(x => x == true || x == false)
                .WithMessage("SSL must be a valid boolean value.");

            RuleFor(x => x.EmilParameterType)
                 .Must(value => Enum.IsDefined(typeof(EmailParameterTypes), value))
                 .WithMessage("EmilParameterType must be a valid value in EmailParameterTypes.");
        }
    }
}
