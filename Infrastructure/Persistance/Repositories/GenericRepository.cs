using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly OutfitIdentityDbContext _context;

        public GenericRepository(OutfitIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity?> GetByIdAsync(int id)
            => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _context.Set<TEntity>().ToListAsync();


        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity?> GetWithSpecAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        // Implementation
        public async Task AddAsync(TEntity entity)
            => await _context.Set<TEntity>().AddAsync(entity);

        public void Update(TEntity entity)
            => _context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity)
            => _context.Set<TEntity>().Remove(entity);

        public async Task<int> CountAsync(ISpecifications<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

  
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }

 
    }
}
