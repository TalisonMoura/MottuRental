using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuRental.Data.Context.Configurations.Base;

public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.Property(x => x.CreatedAt)
                   .HasDefaultValueSql("now()")
                   .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Save);
    }
}