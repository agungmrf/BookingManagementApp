using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

public class CreateEducationValidator : AbstractValidator<CreateEducationDto>
{
    public CreateEducationValidator()
    {
        RuleFor(e => e.Guid)
            .NotEmpty(); // Validasi agar Guid tidak kosong

        RuleFor(e => e.Major)
            .NotEmpty() // Validasi agar Major tidak kosong
            .MaximumLength(100)
            .WithMessage("Major must not exceed 100 characters."); // Validasi agar Major tidak melebihi 100 karakter

        RuleFor(e => e.Degree)
            .NotEmpty() // Validasi agar Degree tidak kosong
            .MaximumLength(100)
            .WithMessage("Degree must not exceed 100 characters."); // Validasi agar Degree tidak melebihi 100 karakter

        RuleFor(e => e.Gpa)
            .NotEmpty(); // Validasi agar Gpa tidak kosong

        RuleFor(e => e.UniversityGuid)
            .NotEmpty(); // Validasi agar UniversityGuid tidak kosong
    }
}