using MottuRental.Data.Context;
using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Core.Models;
using MottuRental.Domain.Interfaces.Repository.Base;

namespace MottuRental.Data.Repository.Base;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : Entity
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual IQueryable<T> ExecuteQuery { get => _dbSet; }

    public virtual IQueryable<T> ExecuteQueryAsNoTracking { get => _dbSet.AsNoTracking(); }

    public async Task<T> RegisterAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));

    public async Task<bool> ExistsAsync(Guid id) => await _dbSet.AnyAsync(x => x.Id.Equals(id));

    public async Task<int> DeleteAsync(Guid id) => await _dbSet.Where(x => x.Id.Equals(id)).ExecuteDeleteAsync();

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}