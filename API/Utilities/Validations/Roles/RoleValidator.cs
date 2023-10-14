using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidator : AbstractValidator<RoleDto>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty() // Validasi bahwa nama role tidak boleh kosong
            .MaximumLength(100)
            .WithMessage(
                "Role name must not exceed 100 characters."); // Validasi bahwa nama role tidak boleh lebih dari 100 karakter
    }
}