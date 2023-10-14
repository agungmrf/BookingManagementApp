using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoles;

public class CreateAccountRoleValidator : AbstractValidator<CreateAccountRoleDto>
{
    public CreateAccountRoleValidator()
    {
        RuleFor(acc => acc.AccountGuid)
            .NotEmpty(); // Validasi agar AccountGuid tidak boleh kosong

        RuleFor(acc => acc.RoleGuid)
            .NotEmpty(); // Validasi agar RoleGuid tidak boleh kosong
    }
}