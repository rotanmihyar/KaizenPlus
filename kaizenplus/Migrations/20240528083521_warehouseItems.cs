using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kaizenplus.Migrations
{
    public partial class warehouseItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarehouseItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    SKU = table.Column<string>(type: "TEXT", nullable: true),
                    Quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    CostPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    MSRPPrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    WarehouseId = table.Column<long>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_WarehouseItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarehouseItem_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseItem_Users_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseItem_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarehouseItem_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 190, 247, 154, 193, 153, 218, 178, 199, 132, 13, 39, 44, 87, 229, 149, 12, 139, 125, 121, 179, 73, 116, 161, 59, 101, 159, 196, 139, 210, 170, 175, 79, 45, 15, 103, 191, 130, 10, 20, 78, 180, 152, 210, 48, 111, 112, 120, 244, 5, 159, 72, 109, 12, 202, 225, 211, 108, 156, 141, 102, 188, 127, 89, 234 }, new byte[] { 173, 146, 185, 223, 241, 235, 40, 208, 13, 89, 63, 157, 111, 247, 249, 250, 138, 114, 114, 164, 235, 187, 125, 173, 82, 156, 253, 194, 83, 215, 184, 172, 233, 11, 55, 65, 21, 239, 153, 225, 157, 174, 58, 92, 48, 132, 25, 220, 48, 3, 141, 38, 157, 32, 116, 123, 111, 41, 200, 185, 224, 59, 227, 70, 240, 190, 5, 210, 140, 16, 29, 119, 66, 7, 171, 78, 118, 124, 122, 31, 238, 114, 100, 135, 184, 243, 122, 140, 34, 64, 46, 138, 76, 209, 170, 8, 181, 65, 151, 62, 166, 98, 93, 146, 208, 36, 15, 145, 2, 189, 159, 171, 11, 153, 143, 20, 156, 197, 203, 16, 234, 157, 6, 134, 53, 23, 148, 146 } });

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItem_CreatedById",
                table: "WarehouseItem",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItem_DeletedById",
                table: "WarehouseItem",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItem_UpdatedById",
                table: "WarehouseItem",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_WarehouseItem_WarehouseId",
                table: "WarehouseItem",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarehouseItem");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8240573d-becc-4aae-b2ab-974979de96a1"),
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 157, 68, 78, 116, 244, 25, 154, 9, 121, 242, 173, 188, 6, 191, 56, 166, 98, 152, 213, 11, 222, 17, 11, 184, 79, 105, 229, 200, 87, 56, 186, 118, 192, 185, 179, 109, 108, 33, 175, 251, 116, 212, 45, 112, 21, 133, 0, 217, 10, 187, 206, 151, 80, 86, 133, 79, 216, 71, 230, 127, 58, 30, 138, 235 }, new byte[] { 156, 200, 63, 182, 150, 178, 42, 13, 162, 26, 216, 55, 199, 167, 180, 1, 14, 75, 247, 206, 37, 210, 103, 26, 167, 220, 155, 199, 214, 118, 94, 104, 79, 221, 117, 78, 48, 81, 110, 168, 213, 107, 201, 166, 135, 180, 79, 62, 80, 168, 220, 23, 85, 213, 233, 78, 154, 172, 117, 131, 186, 143, 109, 233, 123, 219, 40, 168, 147, 184, 58, 8, 2, 66, 22, 16, 168, 22, 69, 49, 140, 182, 128, 113, 86, 25, 57, 152, 165, 11, 203, 116, 111, 169, 178, 15, 175, 139, 147, 104, 138, 161, 99, 219, 42, 246, 52, 61, 153, 135, 190, 19, 27, 227, 165, 178, 126, 107, 251, 41, 98, 117, 66, 55, 77, 108, 92, 181 } });
        }
    }
}
