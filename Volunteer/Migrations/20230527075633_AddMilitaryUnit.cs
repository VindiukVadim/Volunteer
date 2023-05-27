using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunteer.Migrations
{
    /// <inheritdoc />
    public partial class AddMilitaryUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MilitaryUnitId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MilitaryUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainSoldierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MilitaryUnits", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MilitaryUnitId",
                table: "AspNetUsers",
                column: "MilitaryUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers",
                column: "MilitaryUnitId",
                principalTable: "MilitaryUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MilitaryUnits_MilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MilitaryUnits");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_MilitaryUnitId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MilitaryUnitId",
                table: "AspNetUsers");
        }
    }
}
