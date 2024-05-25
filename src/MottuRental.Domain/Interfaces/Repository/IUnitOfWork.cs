namespace MottuRental.Domain.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    Task<bool> SaveChangesAsync();
}
