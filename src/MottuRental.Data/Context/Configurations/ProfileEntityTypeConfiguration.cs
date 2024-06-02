using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Models.AccessControl;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuRental.Domain.Models;

namespace MottuRental.Data.Context.Configurations;

public class ProfileEntityTypeConfiguration : EntityTypeConfiguration<Profile>
{
    public override void Configure(EntityTypeBuilder<Profile> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Name)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.HasOne(x => x.User)
            .WithOne(x => x.Profile)
            .HasForeignKey<User>(x => x.ProfileId);

        builder.HasOne(x => x.Driver)
            .WithOne(x => x.Profile)
            .HasForeignKey<Driver>(x => x.ProfileId);

        builder.HasData(ProfileData);
    }

    private static List<Profile> ProfileData => new()
    {
        new("Driver") { Id = Guid.Parse("190a1b82-93bb-43d8-bd7d-8077cef7c786") },
        new("Manager") { Id = Guid.Parse("a29a7c19-0569-402a-b773-1db0d7903cf1") },
        new("Financial") { Id = Guid.Parse("5a03b605-2c73-4cd3-83cb-b7bd3b1bd265") },
    };
}