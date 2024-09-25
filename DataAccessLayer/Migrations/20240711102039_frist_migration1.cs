using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class frist_migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams__pointsService__pointsServiceID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams__pointsServiceID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "_pointsServiceID",
                table: "Teams");

            migrationBuilder.AlterColumn<int>(
                name: "MatchDatesID",
                table: "Teams",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "_pointsService",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX__pointsService_TeamID",
                table: "_pointsService",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK__pointsService_Teams_TeamID",
                table: "_pointsService",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams",
                column: "MatchDatesID",
                principalTable: "MatchDates",
                principalColumn: "MatchDatesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__pointsService_Teams_TeamID",
                table: "_pointsService");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX__pointsService_TeamID",
                table: "_pointsService");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "_pointsService");

            migrationBuilder.AlterColumn<int>(
                name: "MatchDatesID",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "_pointsServiceID",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams__pointsServiceID",
                table: "Teams",
                column: "_pointsServiceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_MatchDates_MatchDatesID",
                table: "Teams",
                column: "MatchDatesID",
                principalTable: "MatchDates",
                principalColumn: "MatchDatesID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams__pointsService__pointsServiceID",
                table: "Teams",
                column: "_pointsServiceID",
                principalTable: "_pointsService",
                principalColumn: "_pointsServiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
