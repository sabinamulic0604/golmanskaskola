using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolSkola.Migrations
{
    /// <inheritdoc />
    public partial class galerijadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "galerija",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ime = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fajl = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    vidljivost = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__galerija__3213E83F5241C0D6", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "galerija");
        }
    }
}
