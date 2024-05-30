using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kaizenplus.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: false),
                    RefreshToken = table.Column<string>(type: "TEXT", nullable: true),
                    Picture = table.Column<string>(type: "TEXT", nullable: true),
                    RefreshTokenValidUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsVerified = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Management" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Auditor" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "CreatedDate", "DateOfBirth", "Email", "FirstName", "IsVerified", "LastLoginDate", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "Picture", "RefreshToken", "RefreshTokenValidUntil", "Username" },
                values: new object[] { new Guid("8240573d-becc-4aae-b2ab-974979de96a1"), true, new DateTime(1989, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@happywarehouse.com", "system", true, null, "admin", new byte[] { 16, 166, 184, 83, 125, 25, 246, 113, 187, 67, 23, 247, 238, 80, 5, 246, 44, 158, 130, 87, 81, 103, 206, 160, 31, 168, 46, 192, 84, 132, 30, 191, 232, 208, 234, 154, 194, 144, 224, 112, 58, 249, 39, 72, 222, 61, 207, 175, 14, 17, 118, 215, 212, 204, 222, 148, 145, 208, 43, 171, 2, 63, 130, 162 }, new byte[] { 194, 148, 148, 64, 217, 88, 55, 149, 187, 44, 5, 149, 228, 229, 196, 153, 16, 30, 204, 41, 178, 30, 190, 93, 54, 38, 45, 14, 248, 96, 160, 123, 164, 72, 189, 98, 101, 72, 90, 29, 57, 13, 251, 53, 168, 123, 68, 101, 198, 251, 53, 29, 56, 56, 161, 14, 142, 80, 230, 58, 169, 52, 206, 254, 150, 63, 26, 41, 41, 178, 180, 147, 51, 31, 223, 225, 130, 49, 183, 245, 55, 158, 170, 13, 194, 178, 128, 104, 132, 205, 253, 170, 57, 177, 54, 225, 34, 148, 25, 158, 201, 82, 90, 212, 161, 169, 134, 108, 217, 209, 242, 88, 111, 32, 86, 193, 5, 181, 92, 244, 253, 17, 77, 55, 232, 227, 50, 23 }, "07950430205", null, null, null, "admin@happywarehouse.com" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 1, new Guid("8240573d-becc-4aae-b2ab-974979de96a1") });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
