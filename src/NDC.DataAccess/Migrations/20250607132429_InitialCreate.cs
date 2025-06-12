using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeteoriteClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeteoriteClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meteorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false),
                    MeteoriteId = table.Column<int>(type: "int", nullable: false),
                    NameType = table.Column<string>(type: "varchar(32)", nullable: false),
                    MeteoriteClassId = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FallType = table.Column<string>(type: "varchar(32)", nullable: false),
                    ObservationYear = table.Column<DateOnly>(type: "date", nullable: true),
                    Reclat = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Reclong = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GeolocationType = table.Column<string>(type: "varchar(32)", nullable: true),
                    ComputedRegionCbhk = table.Column<int>(type: "int", nullable: true),
                    ComputedRegionNnqa = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meteorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meteorites_MeteoriteClasses_MeteoriteClassId",
                        column: x => x.MeteoriteClassId,
                        principalTable: "MeteoriteClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeteoriteClasses_Name",
                table: "MeteoriteClasses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_MeteoriteClassId",
                table: "Meteorites",
                column: "MeteoriteClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Meteorites_MeteoriteId",
                table: "Meteorites",
                column: "MeteoriteId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Meteorites");

            migrationBuilder.DropTable(
                name: "MeteoriteClasses");
        }
    }
}
