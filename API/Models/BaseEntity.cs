using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public abstract class BaseEntity // digunakan sebagai base class untuk entitas lain.
{
    [Key] [Column("guid")] public Guid Guid { get; set; } // Primary Key.

    [Column("created_date")] public DateTime CreatedDate { get; set; }

    [Column("modified_date")] public DateTime ModifiedDate { get; set; }
}