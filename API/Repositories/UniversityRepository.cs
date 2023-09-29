using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

public class UniversityRepository : IUniversityRepository // Implementasi interface repository untuk model University.
{
    private readonly BookingManagementDbContext _context; // Untuk menyimpan instance dari BookingManagementDbContext.

    public UniversityRepository(BookingManagementDbContext context)
    {
        _context = context;
    }
    
    public IEnumerable<University> GetAll() 
    {
        return _context.Set<University>().ToList();
    }
    
    public University? GetByGuid(Guid guid)
    {
        return _context.Set<University>().Find(guid);
    }

    public University? Create(University university)
    {
        try
        {
            _context.Set<University>().Add(university);
            _context.SaveChanges(); // Untuk menyimpan perubahan data ke database.
            return university;
        }
        catch
        {
            return null;
        }
    }

    // Untuk mengupdate data university.
    public bool Update(University university)
    {
        try
        {
            _context.Set<University>().Update(university);
            _context.SaveChanges(); // Untuk menyimpan perubahan data ke database.
            return true;
        }
        catch
        {
            return false;
        }
    }

    // Untuk menghapus data university.
    public bool Delete(University university)
    {
        try
        {
            _context.Set<University>().Remove(university);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}