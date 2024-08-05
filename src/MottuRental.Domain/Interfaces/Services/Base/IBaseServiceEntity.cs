using MottuRental.Domain.Core.Models;

namespace MottuRental.Domain.Interfaces.Services.Base;

public interface IBaseServiceEntity<T> where T : Entity
{
    Task<List<T>> GetBySqlQueryAsync(string query, CancellationToken cancellationToken = default);
    Task<T> RegisterAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    IQueryable<T> ExecuteQuery { get; }
    IQueryable<T> ExecuteQueryAsNoTracking { get; }
}