using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Area_Size",
                table: "RealES",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "RealES",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RoomID",
                table: "RealES",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    N_LivingRoom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Bathroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Garage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Bedroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Kitchen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N_Floors = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealESId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealES_RoomID",
                table: "RealES",
                column: "RoomID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Rooms_RoomID",
                table: "RealES");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RealES_RoomID",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "Area_Size",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "RealES");
        }
    }
}
