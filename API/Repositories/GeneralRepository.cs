using API.Contracts;
using API.Data;
using API.Utilities.Handler;

namespace API.Repositories;

// Implementasi inheritance dari interface IGeneralRepository. where TEntity digunakan untuk memastikan bahwa TEntity adalah sebuah class.
public class GeneralRepository<TEntity> : IGeneralRepository<TEntity> where TEntity : class 
{
    // Membuat constructor untuk menginisialisasi context.
    public readonly BookingManagementDbContext _context;

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
        var entity = _context.Set<TEntity>().Find(guid);
        _context.ChangeTracker.Clear(); // Untuk menghapus cache dari entity yang diambil.
        return entity;
    }

    public TEntity? Create(TEntity entity)
    {
        try
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges(); // Untuk menyimpan perubahan data ke database.
            return entity;
        }
        catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
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
        catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
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
        catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }
}