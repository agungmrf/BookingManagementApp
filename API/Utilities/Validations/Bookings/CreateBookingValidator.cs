using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Bookings;

public class CreateBookingValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingValidator()
    {
        RuleFor(b => b.StartDate)
            .NotEmpty() // Validasi apakah StartDate tidak kosong
            .GreaterThanOrEqualTo(DateTime.Now); // Validasi apakah StartDate lebih besar atau sama dengan DateTime.Now

        RuleFor(b => b.EndDate)
            .NotEmpty() // Validasi apakah EndDate tidak kosong
            .GreaterThanOrEqualTo(b =>
                b.StartDate.AddHours(
                    +1)); // Validasi apakah EndDate lebih besar atau sama dengan StartDate.AddHours(+1)

        RuleFor(b => b.Status)
            .NotNull() // Validasi apakah Status tidak null
            .IsInEnum(); // Validasi apakah Status berada di dalam enum

        RuleFor(b => b.Remarks)
            .NotEmpty(); // Validasi apakah Remarks tidak kosong

        RuleFor(b => b.RoomGuid)
            .NotEmpty(); // Validasi apakah RoomGuid tidak kosong

        RuleFor(b => b.EmployeeGuid)
            .NotEmpty(); // Validasi apakah EmployeeGuid tidak kosong
    }
}