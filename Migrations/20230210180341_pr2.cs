using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolSkola.Migrations
{
    /// <inheritdoc />
    public partial class pr2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "vijesti",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naslov = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    podnaslov = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    tekst = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    baner = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vidljivost = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vijesti__3213E83FDC62FAAF", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__vijesti__B8B514928CD07B2B",
                table: "vijesti",
                column: "podnaslov",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vijesti");
        }
    }
}
