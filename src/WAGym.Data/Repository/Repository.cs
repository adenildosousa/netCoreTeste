using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WAGym.Data.Data;
using WAGym.Data.Repository.Interfaces;

namespace WAGym.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            return entity;
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }
        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return entities;
        }
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }
        public virtual IEnumerable<TEntity> GetAll() => _dbSet.ToList();
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> expression) => _dbSet.Where(expression).ToList();
        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.Where(expression).ToListAsync();
        public virtual TEntity? Get(Expression<Func<TEntity, bool>> expression) => _dbSet.FirstOrDefault(expression);
        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.FirstOrDefaultAsync(expression);
        public virtual bool Any(Expression<Func<TEntity, bool>> expression) => _dbSet.Any(expression);
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.AnyAsync(expression);
        public virtual void Update(TEntity entity) => _dbSet.Update(entity);
        public void UpdateRange(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);
        public virtual void Delete(TEntity entity) => _dbSet.Remove(entity);
        public virtual void DeleteRange(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);
        public virtual bool Save() => _context.SaveChanges() > 0;
        public virtual async Task<bool> SaveAsync() => await _context.SaveChangesAsync() > 0;
        public int Count(Expression<Func<TEntity, bool>> expression) => _dbSet.Count(expression);
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression) => await _dbSet.CountAsync(expression);
    }
}
