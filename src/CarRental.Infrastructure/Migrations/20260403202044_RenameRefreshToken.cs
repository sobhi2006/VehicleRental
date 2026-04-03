using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TokenHash",
                table: "RefreshTokens",
                newName: "RefreshTokenHash");

            migrationBuilder.RenameColumn(
                name: "ReplacedByTokenHash",
                table: "RefreshTokens",
                newName: "ReplacedByRefreshTokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_TokenHash",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_RefreshTokenHash");

            migrationBuilder.AddForeignKey(
                name: "FK_RevokedAccessTokens_AspNetUsers_UserId",
                table: "RevokedAccessTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RevokedAccessTokens_AspNetUsers_UserId",
                table: "RevokedAccessTokens");

            migrationBuilder.RenameColumn(
                name: "ReplacedByRefreshTokenHash",
                table: "RefreshTokens",
                newName: "ReplacedByTokenHash");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenHash",
                table: "RefreshTokens",
                newName: "TokenHash");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_RefreshTokenHash",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_TokenHash");
        }
    }
}
