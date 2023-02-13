using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GolSkola.Migrations
{
    /// <inheritdoc />
    public partial class Profilna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "golmani",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    godiste = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    klub = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    slika = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    vidljivost = table.Column<int>(type: "int", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__golmani__3213E83F40D54D8B", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "golmani");
        }
    }
}
