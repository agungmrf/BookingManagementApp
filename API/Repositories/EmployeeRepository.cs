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
}