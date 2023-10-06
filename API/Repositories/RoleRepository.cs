using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IRoleRepository.
public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(BookingManagementDbContext context) : base(context)
    {
    }
    
    public Guid? getDefaultRoleGuid() // Mengambil role user.
    {
        // Mengambil role user berdasarkan nama role.
        return _context.Set<Role>().FirstOrDefault(role => role.Name == "user")?.Guid;
    }
}