using Microsoft.EntityFrameworkCore;
using Relive.Server.Core.Entities;
using Relive.Server.Core.Interfaces;
using Relive.Server.Core.UserAggregate;
using Relive.Server.Infrastructure.Data;
using Relive.Server.Infrastructure.Specification;
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

        public async Task DeleteAsync(object id)
        {
            TEntity entity = await _context.Set<TEntity>().FindAsync(id);
            _context.Set<TEntity>().Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            return await SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
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

        public Task<TEntity> GetByOwnerIdAsync(Guid id)
        {
            return _context.Set<TEntity>().Where(x => x.OwnerId == id).FirstOrDefaultAsync();
        }
    }
}
