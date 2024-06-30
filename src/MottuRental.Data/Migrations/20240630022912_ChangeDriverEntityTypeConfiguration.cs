using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MottuRental.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDriverEntityTypeConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Driver_ProfileId",
                table: "Driver");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11d2043e-6adc-4365-9f6e-3c7ac8992327"),
                column: "Password",
                value: "$2a$11$8yHpEnm7JaDDauZnINR67O5UQsg9YR8/AkG9cu5nePBKJkAz9n9IS");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_ProfileId",
                table: "Driver",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Driver_ProfileId",
                table: "Driver");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("11d2043e-6adc-4365-9f6e-3c7ac8992327"),
                column: "Password",
                value: "$2a$11$uqFPB9xZruQDyT4FP/kYfeVnhzlIyHCF090.o5OFm5p9J4CngcR7.");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_ProfileId",
                table: "Driver",
                column: "ProfileId",
                unique: true);
        }
    }
}
