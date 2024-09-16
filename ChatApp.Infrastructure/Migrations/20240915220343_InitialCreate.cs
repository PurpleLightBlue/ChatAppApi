using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    RoomName = table.Column<string>(type: "TEXT", nullable: false),
                    RoomId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.RoomName);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "ChatRoomUser",
                columns: table => new
                {
                    ChatRoomRoomName = table.Column<string>(type: "TEXT", nullable: false),
                    MembersUsername = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRoomUser", x => new { x.ChatRoomRoomName, x.MembersUsername });
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_ChatRooms_ChatRoomRoomName",
                        column: x => x.ChatRoomRoomName,
                        principalTable: "ChatRooms",
                        principalColumn: "RoomName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatRoomUser_Users_MembersUsername",
                        column: x => x.MembersUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SenderName = table.Column<string>(type: "TEXT", nullable: false),
                    ChatRoomName = table.Column<string>(type: "TEXT", nullable: false),
                    ChatRoomRoomName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_ChatRooms_ChatRoomName",
                        column: x => x.ChatRoomName,
                        principalTable: "ChatRooms",
                        principalColumn: "RoomName",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_ChatRooms_ChatRoomRoomName",
                        column: x => x.ChatRoomRoomName,
                        principalTable: "ChatRooms",
                        principalColumn: "RoomName",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderName",
                        column: x => x.SenderName,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomUser_MembersUsername",
                table: "ChatRoomUser",
                column: "MembersUsername");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomName",
                table: "Messages",
                column: "ChatRoomName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatRoomRoomName",
                table: "Messages",
                column: "ChatRoomRoomName");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderName",
                table: "Messages",
                column: "SenderName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatRoomUser");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
