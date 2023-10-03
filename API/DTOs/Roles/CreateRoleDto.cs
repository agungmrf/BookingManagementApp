using API.Models;

namespace API.DTOs.Roles;

public class CreateRoleDto
{
    public string Name { get; set; } 
    
    public static implicit operator Role(CreateRoleDto createRoleDto)
    {
        return new Role
        {
            Name = createRoleDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }
}