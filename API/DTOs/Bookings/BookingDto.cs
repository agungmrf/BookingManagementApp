using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings;

public class BookingDto
{
    public Guid Guid { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public StatusLevel Status { get; set; }
    public string Remarks { get; set; }
    public Guid RoomGuid { get; set; }
    public Guid EmployeeGuid { get; set; }

    public static explicit operator
        BookingDto(Booking booking) // Operator explicit untuk mengkonversi Booking menjadi BookingDto.
    {
        return new BookingDto // Mengembalikan object BookingDto dengan data dari property Booking.
        {
            Guid = booking.Guid,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            Status = booking.Status,
            Remarks = booking.Remarks,
            RoomGuid = booking.RoomGuid,
            EmployeeGuid = booking.EmployeeGuid
        };
    }

    public static implicit operator
        Booking(BookingDto bookingDto) // Operator implicit untuk mengkonversi BookingDto menjadi Booking.
    {
        return new Booking // Mengembalikan object Booking dengan data dari property BookingDto.
        {
            Guid = bookingDto.Guid,
            StartDate = bookingDto.StartDate,
            EndDate = bookingDto.EndDate,
            Status = bookingDto.Status,
            Remarks = bookingDto.Remarks,
            RoomGuid = bookingDto.RoomGuid,
            EmployeeGuid = bookingDto.EmployeeGuid,
            ModifiedDate = DateTime.Now
        };
    }
}