using API.DTOs.Universities;
using FluentValidation;

namespace API.Utilities.Validations.Universities;

public class UniversityValidator : AbstractValidator<UniversityDto>
{
    public UniversityValidator()
    {
        RuleFor(u => u.Code)
            .NotEmpty() // Validasi Code tidak boleh kosong
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters"); // Validasi Code tidak boleh lebih dari 50 karakter

        RuleFor(u => u.Name)
            .NotEmpty() // Validasi Name tidak boleh kosong
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters"); // Validasi Name tidak boleh lebih dari 100 karakter
    }
}