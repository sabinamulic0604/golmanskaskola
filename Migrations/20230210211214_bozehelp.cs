using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolSkola.Migrations
{
    /// <inheritdoc />
    public partial class bozehelp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__vijesti__B8B514928CD07B2B",
                table: "vijesti");

            migrationBuilder.CreateIndex(
                name: "UQ__vijesti__B8B514928CD07B2B",
                table: "vijesti",
                column: "podnaslov");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__vijesti__B8B514928CD07B2B",
                table: "vijesti");

            migrationBuilder.CreateIndex(
                name: "UQ__vijesti__B8B514928CD07B2B",
                table: "vijesti",
                column: "podnaslov",
                unique: true);
        }
    }
}
