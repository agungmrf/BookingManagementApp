namespace API.Contracts;

public interface IGeneralRepository<TEntity> // TEntity untuk data model yang akan digunakan.
{
    IEnumerable<TEntity> GetAll();
    TEntity? GetByGuid(Guid guid);
    TEntity? Create(TEntity entity);
    bool Update(TEntity entity);
    bool Delete(TEntity entity);
}