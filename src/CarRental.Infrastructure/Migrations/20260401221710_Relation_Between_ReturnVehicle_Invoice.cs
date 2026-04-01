using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relation_Between_ReturnVehicle_Invoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_Invoices_BookingVehicles_BookingId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BookingId",
                table: "Invoices");

            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "ReturnVehicles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ReturnVehicles_InvoiceId",
                table: "ReturnVehicles",
                column: "InvoiceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BookingId",
                table: "Invoices",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BookingVehicles_BookingId",
                table: "Invoices",
                column: "BookingId",
                principalTable: "BookingVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReturnVehicles_Invoices_InvoiceId",
                table: "ReturnVehicles",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReturnVehicles_Invoices_InvoiceId",
                table: "ReturnVehicles");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_BookingVehicles_BookingId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_ReturnVehicles_InvoiceId",
                table: "ReturnVehicles");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_BookingId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "ReturnVehicles");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_BookingId",
                table: "Invoices",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BookingVehicles_BookingId",
                table: "Invoices",
                column: "BookingId",
                principalTable: "BookingVehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
