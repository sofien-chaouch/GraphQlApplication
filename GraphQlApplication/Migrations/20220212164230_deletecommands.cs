﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace GraphQlApplication.Migrations
{
    public partial class deletecommands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Command");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Command",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandLine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HowTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Command", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Command_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Command_PlatformId",
                table: "Command",
                column: "PlatformId");
        }
    }
}
