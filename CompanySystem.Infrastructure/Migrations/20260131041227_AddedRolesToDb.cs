using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52fa9d45-abe0-490c-bafe-c622f5c754e0", "1744f3bd-6687-4cac-8274-ab1ca7e8d34e", "Manager", "MANAGER" },
                    { "af07ded8-87e1-43b7-8e18-c0547bab14d8", "29c2b090-f722-4271-a5f0-bdcd810cc7d4", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52fa9d45-abe0-490c-bafe-c622f5c754e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af07ded8-87e1-43b7-8e18-c0547bab14d8");
        }
    }
}
