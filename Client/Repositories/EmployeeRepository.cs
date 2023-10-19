using API.DTOs.Employees;
using Client.Contracts;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<EmployeeDto, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "Employee/") : base(request)
    {
    }
}