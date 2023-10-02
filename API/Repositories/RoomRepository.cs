using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IRoomRepository.
public class RoomRepository : GeneralRepository<Room>, IRoomRepository
{
    public RoomRepository(BookingManagementDbContext context) : base(context)
    {
    }
}