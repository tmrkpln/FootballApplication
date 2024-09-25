using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchDatesTeams",
                columns: table => new
                {
                    MatchDatesID = table.Column<int>(type: "int", nullable: false),
                    TeamsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDatesTeams", x => new { x.MatchDatesID, x.TeamsID });
                    table.ForeignKey(
                        name: "FK_MatchDatesTeams_MatchDates_MatchDatesID",
                        column: x => x.MatchDatesID,
                        principalTable: "MatchDates",
                        principalColumn: "MatchDatesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchDatesTeams_Teams_TeamsID",
                        column: x => x.TeamsID,
                        principalTable: "Teams",
                        principalColumn: "TeamsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchDatesTeams_TeamsID",
                table: "MatchDatesTeams",
                column: "TeamsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchDatesTeams");
        }
    }
}
