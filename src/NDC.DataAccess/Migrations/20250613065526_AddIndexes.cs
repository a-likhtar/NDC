using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Meteorite_Name",
                table: "Meteorites",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Meteorite_ObservationYear",
                table: "Meteorites",
                column: "ObservationYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Meteorite_Name",
                table: "Meteorites");

            migrationBuilder.DropIndex(
                name: "IX_Meteorite_ObservationYear",
                table: "Meteorites");
        }
    }
}
