using FluentValidation;
using IdentityService.Application.Features.Commands.Users.RegisterUser;

namespace IdentityService.Application.Validations;

public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName Field must not be empty.").MaximumLength(50).WithMessage("FirstName must be a maximum of 50 characters.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Just Enter Letters.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName Field must not be empty.").MaximumLength(50).WithMessage("LastName must be a maximum of 50 characters.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Just Enter Letters.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email Field must not be empty.").MaximumLength(75).WithMessage("Email must be a maximum of 50 characters.").EmailAddress().WithMessage("The entered value must be in Email format.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password Field must not be empty.").MinimumLength(6).WithMessage("Password must be a minimum of 6 characters.").MaximumLength(20).WithMessage("Password must be a maximum of 20 characters.");
    }
}