using MottuRental.Domain.Core.Models;
using MottuRental.Domain.Core.Notifications;
using MottuRental.Domain.Interfaces.Services.Base;
using MottuRental.Domain.Interfaces.Repository.Base;
using MottuRental.Domain.Core.Notifications.Interfaces;

namespace MottuRental.Domain.Services.Base;

public abstract class BaseServiceEntity<T> : BaseService, IBaseServiceEntity<T> where T : Entity
{
    protected IBaseRepository<T> BaseRepository { get; }

    protected BaseServiceEntity(IBaseRepository<T> baseRepository, IHandler<DomainNotification> notifications) : base(notifications)
    {
        BaseRepository = baseRepository;
    }

    public virtual IQueryable<T> ExecuteQuery => BaseRepository.ExecuteQuery;

    public virtual IQueryable<T> ExecuteQueryAsNoTracking => BaseRepository.ExecuteQueryAsNoTracking;

    public async Task<T> RegisterAsync(T entity, CancellationToken cancellationToken = default)
    {
        if (entity.Equals(null))
            return null;

        return await BaseRepository.RegisterAsync(entity, cancellationToken);
    }

    public async Task<List<T>> GetBySqlQueryAsync(string query, CancellationToken cancellationToken = default)
    {
        if (!string.IsNullOrEmpty(query))
        {
            return await BaseRepository.GetBySqlQueryAsync(query, cancellationToken);
        }
        return default;
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id.Equals(null))
            return null;

        return await BaseRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id.Equals(null))
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));

        return await BaseRepository.ExistsAsync(id, cancellationToken);
    }

    public async Task<int> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id.Equals(null))
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));

        return await BaseRepository.DeleteAsync(id, cancellationToken);
    }
}