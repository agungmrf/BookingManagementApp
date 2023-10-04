using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
{
    public AccountRoleValidator()
    {
        RuleFor(acc => acc.AccountGuid)
            .NotEmpty(); // Validasi agar AccountGuid tidak boleh kosong

        RuleFor(acc => acc.RoleGuid)
            .NotEmpty(); // Validasi agar RoleGuid tidak boleh kosong
    }
}