using MottuRental.Domain.Models;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuRental.Data.Context.Configurations;

public class AllocateEntityTypeConfiguration : EntityTypeConfiguration<Allocate>
{
    public override void Configure(EntityTypeBuilder<Allocate> builder)
    {
        base.Configure(builder);
    }
}