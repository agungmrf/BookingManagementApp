using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Accounts;

public class AccountValidator : AbstractValidator<AccountDto>
{
    public AccountValidator()
    {
        RuleFor(a => a.Password)
            .NotEmpty() // Validasi agar password tidak kosong
            .Matches(
                "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$"); // Validasi password harus mengandung huruf besar, huruf kecil, angka, dan simbol

        RuleFor(a => a.Otp)
            .NotEmpty(); // Validasi agar OTP tidak kosong

        RuleFor(a => a.IsUsed)
            .NotEmpty(); // Validasi agar IsUsed tidak kosong

        RuleFor(a => a.ExpiredDate)
            .NotEmpty() // Validasi agar ExpiredDate tidak kosong
            .GreaterThanOrEqualTo(DateTime.Now); // Validasi agar ExpiredDate tidak kurang dari tanggal sekarang
    }
}