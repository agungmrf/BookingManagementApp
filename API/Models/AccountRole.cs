using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

[Table("tb_m_account_roles")]
public class AccountRole : BaseEntity
{
    [Column("account_guid")] public Guid AccountGuid { get; set; } // Foreign Key.

    [Column("role_guid")] public Guid RoleGuid { get; set; } // Foreign Key.

    // Cardinality.
    // One AccountRole has one Role.
    public Role? Role { get; set; }

    // One AccountRole has one Account.
    public Account? Account { get; set; }
}