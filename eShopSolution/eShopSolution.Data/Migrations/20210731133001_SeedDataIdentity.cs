using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedDataIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("3a4834eb-7cac-4ab7-9264-2f3de64691bb"), "7a14a6d4-a3f3-403b-9026-7047fd206484", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("f5db37ae-5f02-4eda-8724-c2ca79be046f"), new Guid("3a4834eb-7cac-4ab7-9264-2f3de64691bb") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f5db37ae-5f02-4eda-8724-c2ca79be046f"), 0, "1c08803e-2ef0-4d69-a7d3-58b011e3d704", new DateTime(2020, 1, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "18521694@gm.uit.edu.vn", true, "Ngọc", "Sơn", false, null, "18521694@gm.uit.edu.vn", "admin", "AQAAAAEAACcQAAAAEOFJB5hCAXp5yBybqtsziw3lrKcy8F1P80GpzuP/ZIAu571Lu/w++/YUgNfhR6iF2Q==", null, false, "", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 31, 20, 30, 0, 724, DateTimeKind.Local).AddTicks(8867));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a4834eb-7cac-4ab7-9264-2f3de64691bb"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("f5db37ae-5f02-4eda-8724-c2ca79be046f"), new Guid("3a4834eb-7cac-4ab7-9264-2f3de64691bb") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("f5db37ae-5f02-4eda-8724-c2ca79be046f"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 7, 31, 20, 28, 57, 562, DateTimeKind.Local).AddTicks(4754));
        }
    }
}
