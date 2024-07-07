using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderpets.Migrations.PostMessages
{
    /// <inheritdoc />
    public partial class AddAdoptedByUserIdToPostMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdoptedByUserId",
                table: "PostMessages",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdoptedByUserId",
                table: "PostMessages");
        }
    }
}
