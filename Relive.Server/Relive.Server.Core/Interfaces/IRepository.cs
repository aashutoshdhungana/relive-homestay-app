using System.Collections.Generic;
using System.Threading.Tasks;

namespace Relive.Server.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification);

        Task<TEntity> GetByIdAsync(object id);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(object id);

        void Delete(TEntity entity);

        Task SaveAsync();
    }
}
