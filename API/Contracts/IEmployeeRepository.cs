using API.Models;

namespace API.Contracts;

// Interface repository untuk model Employee yang mengimplementasi interface IGeneralRepository.
// Memanggil <Employee> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IEmployeeRepository : IGeneralRepository<Employee>
{
    
}