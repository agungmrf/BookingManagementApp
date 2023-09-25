namespace API.Models;

public abstract class BaseEntity
{
    public Guid Guid { get; set; } // Primary Key.
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}