using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wanderpets.Migrations.PostMessages
{
    /// <inheritdoc />
    public partial class MakeAdoptedByUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdoptedByUserId",
                table: "PostMessages",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PostMessages",
                keyColumn: "AdoptedByUserId",
                keyValue: null,
                column: "AdoptedByUserId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "AdoptedByUserId",
                table: "PostMessages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
