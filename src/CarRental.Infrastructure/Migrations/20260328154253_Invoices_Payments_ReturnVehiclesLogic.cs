using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Invoices_Payments_ReturnVehiclesLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId1",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_ReturnVehicles_FeesBanks_ExcessMileageFess",
                table: "ReturnVehicles");

            migrationBuilder.DropIndex(
                name: "IX_ReturnVehicles_ExcessMileageFess",
                table: "ReturnVehicles");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLines_InvoiceId1",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "ExcessMileageFess",
                table: "ReturnVehicles");

            migrationBuilder.DropColumn(
                name: "InvoiceId1",
                table: "InvoiceLines");

            migrationBuilder.AddColumn<long>(
                name: "BookingVehicleId",
                table: "Payments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReturnVehicleFeesBanks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReturnVehicleId = table.Column<long>(type: "bigint", nullable: false),
                    FeesBankId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnVehicleFeesBanks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturnVehicleFeesBanks_FeesBanks_FeesBankId",
                        column: x => x.FeesBankId,
                        principalTable: "FeesBanks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReturnVehicleFeesBanks_ReturnVehicles_ReturnVehicleId",
                        column: x => x.ReturnVehicleId,
                        principalTable: "ReturnVehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingVehicleId",
                table: "Payments",
                column: "BookingVehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnVehicleFeesBanks_FeesBankId",
                table: "ReturnVehicleFeesBanks",
                column: "FeesBankId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnVehicleFeesBanks_ReturnVehicleId_FeesBankId",
                table: "ReturnVehicleFeesBanks",
                columns: new[] { "ReturnVehicleId", "FeesBankId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_BookingVehicles_BookingVehicleId",
                table: "Payments",
                column: "BookingVehicleId",
                principalTable: "BookingVehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_BookingVehicles_BookingVehicleId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "ReturnVehicleFeesBanks");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingVehicleId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "BookingVehicleId",
                table: "Payments");

            migrationBuilder.AddColumn<long>(
                name: "ExcessMileageFess",
                table: "ReturnVehicles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId1",
                table: "InvoiceLines",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnVehicles_ExcessMileageFess",
                table: "ReturnVehicles",
                column: "ExcessMileageFess");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_InvoiceId1",
                table: "InvoiceLines",
                column: "InvoiceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId",
                table: "InvoiceLines",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_Invoices_InvoiceId1",
                table: "InvoiceLines",
                column: "InvoiceId1",
                principalTable: "Invoices",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnVehicles_FeesBanks_ExcessMileageFess",
                table: "ReturnVehicles",
                column: "ExcessMileageFess",
                principalTable: "FeesBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
