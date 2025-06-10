using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NDC.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddHashToMeteorite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "Meteorites",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hash",
                table: "Meteorites");
        }
    }
}
