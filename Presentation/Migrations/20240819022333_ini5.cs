using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class ini5 : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_RealES_CityID",
                table: "RealES");

            migrationBuilder.DropIndex(
                name: "IX_RealES_CountryID",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "RealES");

            migrationBuilder.DropColumn(
                name: "CountryID",
                table: "RealES");

            migrationBuilder.RenameColumn(
                name: "HoodID",
                table: "RealES",
                newName: "AddressID");

            migrationBuilder.RenameIndex(
                name: "IX_RealES_HoodID",
                table: "RealES",
                newName: "IX_RealES_AddressID");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountryID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HoodID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityID",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Hoods_HoodID",
                        column: x => x.HoodID,
                        principalTable: "Hoods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityID",
                table: "Addresses",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryID",
                table: "Addresses",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_HoodID",
                table: "Addresses",
                column: "HoodID");

            migrationBuilder.AddForeignKey(
                name: "FK_RealES_Addresses_AddressID",
                table: "RealES",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealES_Addresses_AddressID",
                table: "RealES");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.RenameColumn(
                name: "AddressID",
                table: "RealES",
                newName: "HoodID");

            migrationBuilder.RenameIndex(
                name: "IX_RealES_AddressID",
                table: "RealES",
                newName: "IX_RealES_HoodID");

            migrationBuilder.AddColumn<string>(
                name: "CityID",
                table: "RealES",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CountryID",
                table: "RealES",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RealES_CityID",
                table: "RealES",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_RealES_CountryID",
                table: "RealES",
                column: "CountryID");

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
    }
}
