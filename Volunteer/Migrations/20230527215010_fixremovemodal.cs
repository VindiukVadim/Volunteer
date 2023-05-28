using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.Migrations
{
    /// <inheritdoc />
    public partial class fixremovemodal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers",
                column: "MilitaryUnitId",
                principalTable: "MilitaryUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers",
                column: "MilitaryUnitId",
                principalTable: "MilitaryUnits",
                principalColumn: "Id");
        }
    }
}
