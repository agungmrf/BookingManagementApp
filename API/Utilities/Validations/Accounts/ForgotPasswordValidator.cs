using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
{
    public ForgotPasswordValidator()
    {
        RuleFor(a => a.Email)
            .EmailAddress() // Validasi email harus sesuai format email
            .NotEmpty().WithMessage("Email is required"); // Validasi email tidak boleh kosong
    }
}