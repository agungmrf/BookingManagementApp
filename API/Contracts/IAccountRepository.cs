using API.Models;

namespace API.Contracts;

// Interface repository untuk model Account yang mengimplementasi interface IGeneralRepository.
// Memanggil <Account> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IAccountRepository : IGeneralRepository<Account>
{
}