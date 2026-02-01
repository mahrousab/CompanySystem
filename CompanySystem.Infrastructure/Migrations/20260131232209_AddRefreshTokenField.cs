using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CompanySystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52fa9d45-abe0-490c-bafe-c622f5c754e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af07ded8-87e1-43b7-8e18-c0547bab14d8");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "88aafa7a-598c-4d09-b7ec-4ef48fd0a350", "ec69ae60-f6c4-448c-832e-61d43c176b24", "Administrator", "ADMINISTRATOR" },
                    { "c3f93bcd-02bb-414b-892e-b2bbdab8cda1", "c39f1e6c-7b19-4b1c-9e9a-995bb97b6ed5", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "88aafa7a-598c-4d09-b7ec-4ef48fd0a350");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f93bcd-02bb-414b-892e-b2bbdab8cda1");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52fa9d45-abe0-490c-bafe-c622f5c754e0", "1744f3bd-6687-4cac-8274-ab1ca7e8d34e", "Manager", "MANAGER" },
                    { "af07ded8-87e1-43b7-8e18-c0547bab14d8", "29c2b090-f722-4271-a5f0-bdcd810cc7d4", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
