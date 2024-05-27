using MottuRental.Data.Context;
using MottuRental.Domain.Interfaces.Repository;

namespace MottuRental.Data.UoW;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}