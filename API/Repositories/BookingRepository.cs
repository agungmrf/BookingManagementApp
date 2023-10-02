using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IBokingRepository.
public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    public BookingRepository(BookingManagementDbContext context) : base(context)
    {
    }
}