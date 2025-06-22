using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Utilities.ValidationRules.UserLoginValidations.UserDtoValidators;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDtos>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username cannot be empty")
                .MinimumLength(5).WithMessage("Username must be at least 5 characters long")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("FullName cannot be empty")
            .MaximumLength(100).WithMessage("FullName must not exceed 100 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
            .MaximumLength(100).WithMessage("Password must not exceed 100 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("ConfirmPassword cannot be empty")
            .Equal(x => x.Password).WithMessage("ConfirmPassword must match the password.");
    }
}
