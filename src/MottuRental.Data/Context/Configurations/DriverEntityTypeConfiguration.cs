using MottuRental.Domain.Enums;
using MottuRental.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MottuRental.Data.Context.Configurations;

public class DriverEntityTypeConfiguration : EntityTypeConfiguration<Driver>
{
    public override void Configure(EntityTypeBuilder<Driver> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.Cnpj)
            .HasColumnType("varchar(14)")
            .IsRequired();

        builder.Property(x => x.NumeroCNH)
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(x => x.CnhType)
            .HasColumnType("varchar(3)")
            .HasConversion(new EnumToStringConverter<CnhType>());

        builder.HasOne(x => x.Allocate)
            .WithOne(x => x.Driver)
            .HasForeignKey<Allocate>(x => x.DriverId);

        builder.HasOne(x => x.Profile)
            .WithMany(x => x.Drivers)
            .HasForeignKey(x => x.ProfileId);
    }
}