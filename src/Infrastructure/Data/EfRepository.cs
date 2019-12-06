using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class EfRepository : IRepository
    {
        protected readonly AppDbContext _dbContext;

        public EfRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> GetByIdAsync<T>(int id) where T : BaseEntity
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return (await ListAsync(spec)).FirstOrDefault();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync<T>() where T : BaseEntity
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<T> AddAsync<T>(T entity) where T : BaseEntity
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task AddAsync<T>(List<T> entities) where T : BaseEntity
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(List<T> entities) where T : BaseEntity
        {
            foreach (var entity in entities)
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : BaseEntity
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(List<T> entities) where T : BaseEntity
        {
            _dbContext.Set<T>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification<T>(ISpecification<T> spec) where T : BaseEntity
        {
            return SpecificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }
    }
}
