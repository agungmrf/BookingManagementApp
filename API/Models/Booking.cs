namespace API.Models;

public class Booking : BaseEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public string Remarks { get; set; }
    public Guid RoomGuid { get; set; } // Foreign Key.
    public Guid EmployeeGuid { get; set; } // Foreign Key.
}