using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class CreateRoomValidator : AbstractValidator<CreateRoomDto>
{
    public CreateRoomValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty() // Validasi bahwa Name tidak boleh kosong
            .MaximumLength(100)
            .WithMessage(
                "Name cannot be longer than 100 characters"); // Validasi bahwa Name tidak boleh lebih dari 100 karakter

        RuleFor(r => r.Floor)
            .NotEmpty(); // Validasi bahwa Floor tidak boleh kosong

        RuleFor(r => r.Capacity)
            .NotEmpty(); // Validasi bahwa Capacity tidak boleh kosong
    }
}