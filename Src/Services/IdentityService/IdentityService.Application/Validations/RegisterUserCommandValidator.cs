using FluentValidation;
using IdentityService.Api.Applications.Features.Commands.Users.RegisterUser;

namespace IdentityService.Api.Applications.Validations;

public class RegisterUserCommandValidator:AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Kulanıcı İsmi Boş olamalıdır.").MaximumLength(50).WithMessage("Kulanıcı ismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Kulanıcı Soyismi Boş olamalıdır.").MaximumLength(50).WithMessage("Kulanıcı Soyismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
        RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş olmamlıdır.").MaximumLength(75).WithMessage("Email Alanı maksimum 75 karakter olamlıdır.").EmailAddress().WithMessage("Girilen deger Email formatinda olmalıdır.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanı boş olmamalıdır.").MinimumLength(6).WithMessage("Şifre alanı en az 6 karakter olamlıdır.").MaximumLength(20).WithMessage("Şifre Alanı en fazla 20 karakter olamlıdır.");
    }
}