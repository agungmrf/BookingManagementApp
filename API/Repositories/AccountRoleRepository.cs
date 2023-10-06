using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories;

// Inherit dari GeneralRepository dan implementasi interface IAccountRoleRepository
public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(BookingManagementDbContext context) : base(context)
    {
    }

    public IEnumerable<Guid> GetRoleGuidsByAccountGuid(Guid accountGuid)
    {
        return _context.Set<AccountRole>().Where(ar => ar.AccountGuid == accountGuid).Select(ar => ar.RoleGuid);
    }
}