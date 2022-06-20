using Relive.Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Relive.Server.Core.Intefaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification);

        Task<TEntity> GetById(object id);

        Task InsertAsync(TEntity entity);

        void Update(TEntity entity);

        Task Delete(object id);

        void Delete(TEntity entity);

        Task SaveAsync();
    }
}
