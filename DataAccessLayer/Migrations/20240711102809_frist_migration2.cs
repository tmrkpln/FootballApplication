using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class frist_migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_MatchDatesID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "MatchDatesID",
                table: "Teams");

            migrationBuilder.AddColumn<int>(
                name: "TeamsID",
                table: "MatchDates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamsID",
                table: "MatchDates");

            migrationBuilder.AddColumn<int>(
                name: "MatchDatesID",
                table: "Teams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_MatchDatesID",
                table: "Teams",
                column: "MatchDatesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams",
                column: "MatchDatesID",
                principalTable: "MatchDates",
                principalColumn: "MatchDatesID");
        }
    }
}
