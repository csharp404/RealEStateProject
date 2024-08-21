using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class testing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Cities_CityID",
                table: "RealES");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Countries_CountryID",
                table: "RealES");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Hoods_HoodID",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "RealES");

            migrationBuilder.AddColumn<string>(
                name: "CityId",
                table: "Hoods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryId",
                table: "Cities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RealESImages",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealESId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealESImages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RealESImages_RealES_RealESId",
                        column: x => x.RealESId,
                        principalTable: "RealES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hoods_CityId",
                table: "Hoods",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryId",
                table: "Cities",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RealESImages_RealESId",
                table: "RealESImages",
                column: "RealESId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Hoods_Cities_CityId",
                table: "Hoods",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Cities_CityID",
                table: "RealES",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Countries_CountryID",
                table: "RealES",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Hoods_HoodID",
                table: "RealES",
                column: "HoodID",
                principalTable: "Hoods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Hoods_Cities_CityId",
                table: "Hoods");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Cities_CityID",
                table: "RealES");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Countries_CountryID",
                table: "RealES");

            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Hoods_HoodID",
                table: "RealES");

            migrationBuilder.DropTable(
                name: "RealESImages");

            migrationBuilder.DropIndex(
                name: "IX_Hoods_CityId",
                table: "Hoods");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Hoods");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "RealES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "RealES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Cities_CityID",
                table: "RealES",
                column: "CityID",
                principalTable: "Cities",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Countries_CountryID",
                table: "RealES",
                column: "CountryID",
                principalTable: "Countries",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Hoods_HoodID",
                table: "RealES",
                column: "HoodID",
                principalTable: "Hoods",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
