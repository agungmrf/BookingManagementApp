using API.Models;

namespace API.Contracts;

// Interface repository untuk model AccountRole yang mengimplementasi interface IGeneralRepository.
// Memanggil <AccountRole> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IAccountRoleRepository : IGeneralRepository<AccountRole> 
{
    IEnumerable<Guid> GetRoleGuidsByAccountGuid(Guid accountGuid);
}