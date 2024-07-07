using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderpets.Migrations.PostMessages
{
    /// <inheritdoc />
    public partial class PostMessagesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RegisterDetails",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserPhone = table.Column<int>(type: "int(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterDetails", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PostMessage = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PostLocation = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PostTag = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    PostFilter = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Images = table.Column<byte[]>(type: "longblob", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMessages_RegisterDetails_UserId",
                        column: x => x.UserId,
                        principalTable: "RegisterDetails",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PostMessages_UserId",
                table: "PostMessages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMessages");

            migrationBuilder.DropTable(
                name: "RegisterDetails");
        }
    }
}
