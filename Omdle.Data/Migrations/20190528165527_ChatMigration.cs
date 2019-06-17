using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Omdle.Data.Migrations
{
    public partial class ChatMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChateMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    SendTime = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MessageOwnerId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChateMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChateMessages_AspNetUsers_MessageOwnerId",
                        column: x => x.MessageOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChateMessages_MessageOwnerId",
                table: "ChateMessages",
                column: "MessageOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChateMessages");
        }
    }
}
