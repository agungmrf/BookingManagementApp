using API.DTOs.Accounts;
using API.Models;

namespace API.DTOs.AccountRoles;

public class AccountRoleDto
{
    public Guid Guid { get; set; }
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static explicit operator AccountRoleDto(AccountRole accountRole) // Operator explicit untuk mengkonversi AccountRole menjadi AccountRoleDto.
    {
        return new AccountRoleDto // Mengembalikan object AccountRoleDto dengan data dari property AccountRole.
        {
            Guid = accountRole.Guid,
            AccountGuid = accountRole.AccountGuid,
            RoleGuid = accountRole.RoleGuid
        };
    }
    
    public static implicit operator AccountRole(AccountRoleDto accountRoleDto) // Operator implicit untuk mengkonversi AccountRoleDto menjadi AccountRole.
    {
        return new AccountRole // Mengembalikan object AccountRole dengan data dari property AccountRoleDto.
        {
            Guid = accountRoleDto.Guid,
            AccountGuid = accountRoleDto.AccountGuid,
            RoleGuid = accountRoleDto.RoleGuid,
            ModifiedDate = DateTime.Now
        };
    }
}