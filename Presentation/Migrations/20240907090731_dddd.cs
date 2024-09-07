using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class dddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RealES_RealESID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RealES_RealESID",
                table: "Comments",
                column: "RealESID",
                principalTable: "RealES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_RealES_RealESID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_RealES_RealESID",
                table: "Comments",
                column: "RealESID",
                principalTable: "RealES",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
