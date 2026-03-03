using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSocial.Migrations
{
    /// <inheritdoc />
    public partial class userProfileFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UsersProfile",
                newName: "IdentityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdentityId",
                table: "UsersProfile",
                newName: "UserId");
        }
    }
}
