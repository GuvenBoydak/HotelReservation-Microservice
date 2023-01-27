using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FakePaymentService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addFakePaymententitydatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CardNumber = table.Column<string>(type: "text", nullable: false),
                    CardExpiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardName = table.Column<string>(type: "text", nullable: false),
                    CardCVV = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CreditCard",
                columns: new[] { "Id", "Amount", "CardCVV", "CardExpiry", "CardName", "CardNumber", "CreatedDate", "DeletedDate", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("2a2b07a1-648c-4d0e-9c85-faec301a81a5"), 100000m, "861", new DateTime(2026, 5, 1, 1, 1, 1, 0, DateTimeKind.Utc), "Deneme", "5444112548076404", new DateTime(2023, 1, 27, 16, 3, 28, 717, DateTimeKind.Utc).AddTicks(324), null, false },
                    { new Guid("a98f7dac-1e48-479b-a727-ff5fe1b875ec"), 100000m, "852", new DateTime(2026, 5, 1, 1, 1, 1, 0, DateTimeKind.Utc), "Test", "5555112548076309", new DateTime(2023, 1, 27, 16, 3, 28, 717, DateTimeKind.Utc).AddTicks(309), null, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCard");
        }
    }
}
