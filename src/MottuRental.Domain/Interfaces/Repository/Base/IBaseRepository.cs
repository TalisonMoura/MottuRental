using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Interfaces.Repository.Base;

public interface IBaseRepository<T> : IDisposable where T : Entity
{
    Task<T> RegisterAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task Delete(T entity);
    IQueryable<T> ExecuteQuery { get; }
    IQueryable<T> ExecuteQueryAsNoTracking { get; }
}