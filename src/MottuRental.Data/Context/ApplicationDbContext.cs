using Microsoft.EntityFrameworkCore;
using MottuRental.Infra.CrossCutting.Commons.Providers;

namespace MottuRental.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, DbContextProvider contextProvider) : DbContext(options)
{
    private readonly DbContextProvider _contextProvider = contextProvider;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseNpgsql(_contextProvider.ConnectionString);

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Model.GetEntityTypes().ToList().ForEach(entityType =>
        {
            entityType.SetTableName(entityType.DisplayName());

            entityType.GetForeignKeys()
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                .ToList()
                .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}