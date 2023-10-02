using API.Contracts;
using API.Data;

namespace API.Repositories;

// Implementasi inheritance dari interface IGeneralRepository. where TEntity digunakan untuk memastikan bahwa TEntity adalah sebuah class.
public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class 
{
    // Membuat constructor untuk menginisialisasi context.
    private readonly BookingManagementDbContext _context;

    // Constructor akan dijalankan ketika class GeneralRepository dipanggil.
    protected GeneralRepository(BookingManagementDbContext context)
    {
        _context = context;
    }

    // Implementasi method GetAll, GetByGuid, Create, Update, dan Delete.
    public IEnumerable<TEntity> GetAll()
    {
        return _context.Set<TEntity>().ToList();
    }

    public TEntity? GetByGuid(Guid guid)
    {
        return _context.Set<TEntity>().Find(guid);
    }

    public TEntity? Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges(); // Untuk menyimpan perubahan data ke database.
            return entity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges(); // Untuk menyimpan perubahan data ke database.
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}