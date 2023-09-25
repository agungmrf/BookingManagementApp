namespace API.Models;

public class AccountRole : BaseEntity
{
    public Guid AccountGuid { get; set; } // Foreign Key.
    public Guid RoleGuid { get; set; } // Foreign Key.
}