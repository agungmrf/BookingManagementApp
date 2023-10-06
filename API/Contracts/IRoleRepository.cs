using API.Models;

namespace API.Contracts;

// Interface repository untuk model Role yang mengimplementasi interface IGeneralRepository.
// Memanggil <Role> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IRoleRepository : IGeneralRepository<Role>
{
    Guid? getDefaultRoleGuid();
}