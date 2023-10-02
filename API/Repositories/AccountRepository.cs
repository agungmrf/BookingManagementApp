using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IAccountRepository.
public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(BookingManagementDbContext context) : base(context)
    {
    }
}