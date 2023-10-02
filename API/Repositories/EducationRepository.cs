using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IEducationRepository.
public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    public EducationRepository(BookingManagementDbContext context) : base(context)
    {
    }
}