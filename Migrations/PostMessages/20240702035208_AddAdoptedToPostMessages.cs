using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderpets.Migrations.PostMessages
{
    /// <inheritdoc />
    public partial class AddAdoptedToPostMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Adopted",
                table: "PostMessages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adopted",
                table: "PostMessages");
        }
    }
}
