using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class RollHistoryInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RollHistories",
                columns: table => new
                {
                    RollHistoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiceNotation = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ChangedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChangedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RollHistories", x => x.RollHistoryId);
                });

            migrationBuilder.CreateTable(
                name: "DiceValues",
                columns: table => new
                {
                    DiceValueId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Pip = table.Column<int>(type: "INTEGER", nullable: false),
                    RollHistoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiceValues", x => x.DiceValueId);
                    table.ForeignKey(
                        name: "FK_DiceValues_RollHistories_RollHistoryId",
                        column: x => x.RollHistoryId,
                        principalTable: "RollHistories",
                        principalColumn: "RollHistoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiceValues_RollHistoryId",
                table: "DiceValues",
                column: "RollHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiceValues");

            migrationBuilder.DropTable(
                name: "RollHistories");
        }
    }
}
