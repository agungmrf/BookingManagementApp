using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IUniversityRepository.
public class UniversityRepository :  GeneralRepository<University>, IUniversityRepository 
{
    public UniversityRepository(BookingManagementDbContext context) : base(context)
    {
    }
    
    public University? GetUniversityByCode(string code)
    {
        return _context.Set<University>().SingleOrDefault(unv => unv.Code == code);
    }
}