using API.Models;

namespace API.Contracts;

// Interface repository untuk model Room yang mengimplementasi interface IGeneralRepository.
// Memanggil <Room> karena IGeneralRepository membutuhkan TEntity sebagai sebuah model.
public interface IRoomRepository : IGeneralRepository<Room>
{
    
}