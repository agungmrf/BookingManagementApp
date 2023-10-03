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
    
    public static explicit operator BookingDto(Booking booking)
    {
        return new BookingDto
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
    
    public static implicit operator Booking(BookingDto bookingDto)
    {
        return new Booking
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