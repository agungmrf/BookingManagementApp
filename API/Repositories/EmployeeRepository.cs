using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IEmployeeRepository.
public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingManagementDbContext context) : base(context)
    {
    }

    // Method untuk mendapatkan NIK terakhir
    public string? GetLastNik()
    {
        // Mengambil data dari database dengan model Employee, kemudian diurutkan berdasarkan NIK secara ascending, lalu diambil data terakhir.
        // LastOrDefault() digunakan untuk mengambil data terakhir
        var lastNik = _context.Set<Employee>().ToList().OrderBy(employee => employee.Nik).LastOrDefault()?.Nik;

        return lastNik;
    }

    public Employee? GetByEmail(string email)
    {
        // Mengambil data dari database dengan model Employee, kemudian diambil data yang emailnya mengandung email yang diberikan
        return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
    }
}