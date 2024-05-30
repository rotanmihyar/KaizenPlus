using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kaizenplus.Migrations
{
    public partial class warehouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    CountryId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedById = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warehouses_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Warehouses_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouses_Users_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Warehouses_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Jordan" });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Germany" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 157, 68, 78, 116, 244, 25, 154, 9, 121, 242, 173, 188, 6, 191, 56, 166, 98, 152, 213, 11, 222, 17, 11, 184, 79, 105, 229, 200, 87, 56, 186, 118, 192, 185, 179, 109, 108, 33, 175, 251, 116, 212, 45, 112, 21, 133, 0, 217, 10, 187, 206, 151, 80, 86, 133, 79, 216, 71, 230, 127, 58, 30, 138, 235 }, new byte[] { 156, 200, 63, 182, 150, 178, 42, 13, 162, 26, 216, 55, 199, 167, 180, 1, 14, 75, 247, 206, 37, 210, 103, 26, 167, 220, 155, 199, 214, 118, 94, 104, 79, 221, 117, 78, 48, 81, 110, 168, 213, 107, 201, 166, 135, 180, 79, 62, 80, 168, 220, 23, 85, 213, 233, 78, 154, 172, 117, 131, 186, 143, 109, 233, 123, 219, 40, 168, 147, 184, 58, 8, 2, 66, 22, 16, 168, 22, 69, 49, 140, 182, 128, 113, 86, 25, 57, 152, 165, 11, 203, 116, 111, 169, 178, 15, 175, 139, 147, 104, 138, 161, 99, 219, 42, 246, 52, 61, 153, 135, 190, 19, 27, 227, 165, 178, 126, 107, 251, 41, 98, 117, 66, 55, 77, 108, 92, 181 } });

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CountryId",
                table: "Warehouses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_CreatedById",
                table: "Warehouses",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_DeletedById",
                table: "Warehouses",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_UpdatedById",
                table: "Warehouses",
                column: "UpdatedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 16, 166, 184, 83, 125, 25, 246, 113, 187, 67, 23, 247, 238, 80, 5, 246, 44, 158, 130, 87, 81, 103, 206, 160, 31, 168, 46, 192, 84, 132, 30, 191, 232, 208, 234, 154, 194, 144, 224, 112, 58, 249, 39, 72, 222, 61, 207, 175, 14, 17, 118, 215, 212, 204, 222, 148, 145, 208, 43, 171, 2, 63, 130, 162 }, new byte[] { 194, 148, 148, 64, 217, 88, 55, 149, 187, 44, 5, 149, 228, 229, 196, 153, 16, 30, 204, 41, 178, 30, 190, 93, 54, 38, 45, 14, 248, 96, 160, 123, 164, 72, 189, 98, 101, 72, 90, 29, 57, 13, 251, 53, 168, 123, 68, 101, 198, 251, 53, 29, 56, 56, 161, 14, 142, 80, 230, 58, 169, 52, 206, 254, 150, 63, 26, 41, 41, 178, 180, 147, 51, 31, 223, 225, 130, 49, 183, 245, 55, 158, 170, 13, 194, 178, 128, 104, 132, 205, 253, 170, 57, 177, 54, 225, 34, 148, 25, 158, 201, 82, 90, 212, 161, 169, 134, 108, 217, 209, 242, 88, 111, 32, 86, 193, 5, 181, 92, 244, 253, 17, 77, 55, 232, 227, 50, 23 } });
        }
    }
}
