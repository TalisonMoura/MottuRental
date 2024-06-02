using Microsoft.EntityFrameworkCore;
using MottuRental.Domain.Models.AccessControl;
using MottuRental.Data.Context.Configurations.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MottuRental.Data.Context.Configurations;

public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserName)
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder.Property(x => x.Document)
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.HasData(UserData);
    }

    private static User UserData => new("Master", "41959373021", BCrypt.Net.BCrypt.HashPassword("Teste@1"), true, Guid.Parse("a29a7c19-0569-402a-b773-1db0d7903cf1")) { Id = Guid.Parse("11d2043e-6adc-4365-9f6e-3c7ac8992327") };
}