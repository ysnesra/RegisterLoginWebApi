using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class addIUsed_TwoFactorAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "ccdf7302-fb0e-45ef-8447-be1b0d4c6e48");

            migrationBuilder.AddColumn<bool>(
                name: "IsSend",
                table: "TwoFactorAuthenticationTransactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3242d7f-a111-4ec8-abe3-cd09ce4766d5", "1b6f8515-5449-46e3-9d5d-9e2efbce6a4e", "user", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a3242d7f-a111-4ec8-abe3-cd09ce4766d5");

            migrationBuilder.DropColumn(
                name: "IsSend",
                table: "TwoFactorAuthenticationTransactions");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ccdf7302-fb0e-45ef-8447-be1b0d4c6e48", "01e72710-982c-4343-9b0a-996ab1637017", "user", null });
        }
    }
}
