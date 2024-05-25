using MottuRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuRental.Data.Context.Configurations;

public class MotorcycleEntityTypeConfiguration : EntityTypeConfiguration<Motorcycle>
{
    public override void Configure(EntityTypeBuilder<Motorcycle> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Plate)
            .HasColumnType("varchar(7)")
            .IsRequired();

        builder.Property(x => x.Model)
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder.HasOne(x => x.Allocate)
            .WithOne(x => x.Motorcycle)
            .HasForeignKey<Allocate>(x => x.MotorcycleId);
    }
}