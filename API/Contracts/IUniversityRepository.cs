using API.Models;

namespace API.Contracts;

// Interface repository untuk model University yang mengimplementasi interface IGeneralRepository.
// Memanggil <University> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IUniversityRepository : IGeneralRepository<University>
{
    University? GetUniversityByCode(string code); // Method untuk mendapatkan data University berdasarkan code
}