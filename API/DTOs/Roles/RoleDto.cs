using API.Models;

namespace API.DTOs.Roles;

public class RoleDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static explicit operator RoleDto(Role role) // Operator explicit untuk mengkonversi Role menjadi RoleDto.
    {
        return new RoleDto // Mengembalikan object RoleDto dengan data dari property Role.
        {
            Guid = role.Guid,
            Name = role.Name
        };
    }

    public static implicit operator Role(RoleDto roleDto) // Operator implicit untuk mengkonversi RoleDto menjadi Role.
    {
        return new Role // Mengembalikan object Role dengan data dari property RoleDto.
        {
            Guid = roleDto.Guid,
            Name = roleDto.Name,
            ModifiedDate = DateTime.Now
        };
    }
}