using MottuRental.Domain.Event;
using Microsoft.EntityFrameworkCore;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuRental.Data.Context.Configurations;

public class MotorcycleEventTypeConfiguration : EntityTypeConfiguration<MotorcycleEvent>
{
    public override void Configure(EntityTypeBuilder<MotorcycleEvent> builder)
    {
        builder.Property(x => x.Motorcycle)
            .HasColumnType("jsonb");

        base.Configure(builder);
    }
}