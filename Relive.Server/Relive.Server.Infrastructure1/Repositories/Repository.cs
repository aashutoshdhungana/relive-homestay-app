using Relive.Server.Core.Entities;
using Relive.Server.Core.Intefaces;
using Relive.Server.Core.Interfaces;
using Relive.Server.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Relive.Server.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity , IAggregateRoot
    {
        private ApplicationContext _context;
        public Repository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Delete(object id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            // TODO: Implement specification based filter.
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(object id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }
    }
}
