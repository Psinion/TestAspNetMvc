using TestAspNetMvc.Data.Models.Base;

namespace TestAspNetMvc.Data.Repositories.Base;

public interface IRepository<TEntity> where TEntity : IEntity
{
    TEntity? GetById(int id);

    IEnumerable<TEntity> GetAll();

    TEntity Add(TEntity item);

    void Update(TEntity item);

    void Remove(int id);
}