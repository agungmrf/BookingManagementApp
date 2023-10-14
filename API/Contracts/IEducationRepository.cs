using API.Models;

namespace API.Contracts;

// Interface repository untuk model Education yang mengimplementasi interface IGeneralRepository.
// Memanggil <Education> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IEducationRepository : IGeneralRepository<Education>
{
}