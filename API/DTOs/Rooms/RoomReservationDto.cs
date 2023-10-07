using API.Utilities.Enums;

namespace API.DTOs.Rooms;

public class RoomReservationDto
{
    // BookingGuid dari BookingRoom
    public Guid BookingGuid { get; set; }
    
    // RoomName, Status, Floor dari Room
    public string RoomName { get; set; }
    public StatusLevel Status { get; set; }
    public int Floor { get; set; }
    
    // BookedBy dari Employee
    public string BookedBy { get; set; }
}