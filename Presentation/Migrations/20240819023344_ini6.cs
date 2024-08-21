using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class ini6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RealES_AddressID",
                table: "RealES");

            migrationBuilder.AddColumn<string>(
                name: "RealESID",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RealES_AddressID",
                table: "RealES",
                column: "AddressID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RealES_AddressID",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "RealESID",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_RealES_AddressID",
                table: "RealES",
                column: "AddressID");
        }
    }
}
