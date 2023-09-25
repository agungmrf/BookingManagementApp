using System.ComponentModel.DataAnnotations.Schema;
using API.Utilities.Enums;

namespace API.Models;

[Table("tb_m_bookings")]
public class Booking : BaseEntity
{
    [Column("start_date")]
    public DateTime StartDate { get; set; }
    [Column("end_date")]
    public DateTime EndDate { get; set; }
    [Column("status")]
    public StatusLevel Status { get; set; }
    [Column("remarks", TypeName = "nvarchar(max)")]
    public string Remarks { get; set; }
    [Column("room_guid")]
    public Guid RoomGuid { get; set; } // Foreign Key.
    [Column("employee_guid")]
    public Guid EmployeeGuid { get; set; } // Foreign Key.
}