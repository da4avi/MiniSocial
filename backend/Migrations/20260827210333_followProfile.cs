using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSocial.Migrations
{
    /// <inheritdoc />
    public partial class followProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Followers",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Following",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Followers",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Following",
                table: "Profiles");
        }
    }
}
