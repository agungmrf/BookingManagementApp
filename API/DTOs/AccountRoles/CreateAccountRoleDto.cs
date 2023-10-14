using API.Models;

namespace API.DTOs.AccountRoles;

public class CreateAccountRoleDto
{
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static implicit operator
        AccountRole(
            CreateAccountRoleDto createAccountRoleDto) // Operator implicit untuk mengkonversi CreateAccountRoleDto menjadi AccountRole.
    {
        return new AccountRole // Mengembalikan object AccountRole dengan data dari property CreateAccountRoleDto.
        {
            AccountGuid = createAccountRoleDto.AccountGuid,
            RoleGuid = createAccountRoleDto.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}