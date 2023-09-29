using API.Models;

namespace API.Contracts;

public interface IUniversityRepository // Interface repository untuk model University.
{
    IEnumerable<University> GetAll(); // IEnumerable untuk menampung banyak data.
    University? GetByGuid(Guid guid);
    University? Create(University university);
    bool Update(University university);
    bool Delete(University university);
}