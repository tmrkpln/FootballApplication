using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class edit_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplacementWinRate",
                table: "MatchDates");

            migrationBuilder.DropColumn(
                name: "HomeOwnerWinRate",
                table: "MatchDates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplacementWinRate",
                table: "MatchDates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeOwnerWinRate",
                table: "MatchDates",
                type: "int",
                nullable: true);
        }
    }
}
