using API.Models;

namespace API.Contracts;

// Interface repository untuk model Employee yang mengimplementasi interface IGeneralRepository.
// Memanggil <Employee> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    string GetLastNik(); // Method untuk mendapatkan NIK terakhir
    Employee? GetByEmail(string email); // Method untuk mendapatkan data Employee berdasarkan email
}