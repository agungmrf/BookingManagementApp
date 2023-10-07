namespace API.DTOs.Bookings;

public class BookingDetailDto
{
    public Guid Guid { get; set; }
    // Ambil NIK dan Name dari Employee
    public string BookedNIK { get; set; }
    public string BookedBy { get; set; }
    // Ambil Name dari Room
    public string RoomName { get; set; }
    // Ambil StartDate, EndDate, Status, dan Remarks dari Booking
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public string Remarks { get; set; }
}