using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSocial.Migrations
{
    /// <inheritdoc />
    public partial class likePost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikedPosts",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedPosts",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");
        }
    }
}
