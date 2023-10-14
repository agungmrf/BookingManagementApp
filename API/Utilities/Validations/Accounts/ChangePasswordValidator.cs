using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(a => a.Email)
            .EmailAddress()
            .NotEmpty().WithMessage("Email is required");

        RuleFor(a => a.Otp)
            .NotEmpty().WithMessage("OTP is required");

        RuleFor(a => a.NewPassword)
            .NotEmpty().WithMessage("New password is required")
            .Matches(
                "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$") // Validasi password harus mengandung huruf besar, huruf kecil, angka, dan simbol
            .WithMessage(
                "Password must contain at least 8 characters, one uppercase, one lowercase, one number and one special case character");

        RuleFor(a => a.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required")
            .Equal(e => e.NewPassword).WithMessage("Confirm password must be the same as new password");
    }
}