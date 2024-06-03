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

    public async Task<T> RegisterAsync(T entity)
    {
        if (entity.Equals(null))
            return null;

        return await BaseRepository.RegisterAsync(entity);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        if (id.Equals(null))
            return null;

        return await BaseRepository.GetByIdAsync(id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        if (id.Equals(null))
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));

        return await BaseRepository.ExistsAsync(id);
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        if (id.Equals(null))
            ArgumentException.ThrowIfNullOrEmpty(nameof(id));

        return await BaseRepository.DeleteAsync(id);
    }
}