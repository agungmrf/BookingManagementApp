using API.Models;

namespace API.DTOs.Roles;

public class CreateRoleDto
{
    public string Name { get; set; } 
    
    public static implicit operator Role(CreateRoleDto createRoleDto) // Operator implicit untuk mengkonversi CreateRoleDto menjadi Role.
    {
        return new Role // Mengembalikan object Role dengan data dari property CreateRoleDto.
        {
            Name = createRoleDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}