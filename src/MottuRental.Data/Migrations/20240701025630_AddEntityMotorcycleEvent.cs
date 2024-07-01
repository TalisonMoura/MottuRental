using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityMotorcycleEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MotorcycleEvent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Motorcycle = table.Column<string>(type: "jsonb", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorcycleEvent", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11d2043e-6adc-4365-9f6e-3c7ac8992327"),
                column: "Password",
                value: "$2a$11$8Act8BIQNFZj7hCLLPLP3eep/uaKZNDgkGf4OzZq8OOd2O0I6vY9i");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotorcycleEvent");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11d2043e-6adc-4365-9f6e-3c7ac8992327"),
                column: "Password",
                value: "$2a$11$8yHpEnm7JaDDauZnINR67O5UQsg9YR8/AkG9cu5nePBKJkAz9n9IS");
        }
    }
}
