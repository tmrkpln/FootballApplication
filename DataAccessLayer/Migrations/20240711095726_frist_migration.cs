using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class frist_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchDates",
                columns: table => new
                {
                    MatchDatesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Displacement = table.Column<int>(type: "int", nullable: false),
                    HomeOwner = table.Column<int>(type: "int", nullable: false),
                    DisplacementScore = table.Column<int>(type: "int", nullable: false),
                    HomeOwnerScore = table.Column<int>(type: "int", nullable: false),
                    Winning = table.Column<int>(type: "int", nullable: false),
                    WinRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDates", x => x.MatchDatesID);
                });

            migrationBuilder.CreateTable(
                name: "_pointsService",
                columns: table => new
                {
                    _pointsServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    League = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Played = table.Column<int>(type: "int", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Draws = table.Column<int>(type: "int", nullable: false),
                    Losses = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalDifference = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    WinRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pointsService", x => x._pointsServiceID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundingYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamColours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _pointsServiceID = table.Column<int>(type: "int", nullable: false),
                    MatchDatesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamsID);
                    table.ForeignKey(
                        name: "FK_Teams_MatchDates_MatchDatesID",
                        column: x => x.MatchDatesID,
                        principalTable: "MatchDates",
                        principalColumn: "MatchDatesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams__pointsService__pointsServiceID",
                        column: x => x._pointsServiceID,
                        principalTable: "_pointsService",
                        principalColumn: "_pointsServiceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_MatchDatesID",
                table: "Teams",
                column: "MatchDatesID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams__pointsServiceID",
                table: "Teams",
                column: "_pointsServiceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "MatchDates");

            migrationBuilder.DropTable(
                name: "_pointsService");
        }
    }
}
