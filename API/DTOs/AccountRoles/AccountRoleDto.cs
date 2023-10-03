using API.DTOs.Accounts;
using API.Models;

namespace API.DTOs.AccountRoles;

public class AccountRoleDto
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static explicit operator AccountRoleDto(AccountRole accountRole)
    {
        return new AccountRoleDto
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
    
    public static implicit operator AccountRole(AccountRoleDto accountRoleDto)
    {
        return new AccountRole
        {
            Guid = accountRoleDto.Guid,
            AccountGuid = accountRoleDto.AccountGuid,
            RoleGuid = accountRoleDto.RoleGuid,
            ModifiedDate = DateTime.Now
        };
    }
}