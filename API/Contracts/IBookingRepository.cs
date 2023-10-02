using API.Models;

namespace API.Contracts;

// Interface repository untuk model Booking yang mengimplementasi interface IGeneralRepository.
// Memanggil <Booking> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IBookingRepository : IGeneralRepository<Booking>
{
    
}