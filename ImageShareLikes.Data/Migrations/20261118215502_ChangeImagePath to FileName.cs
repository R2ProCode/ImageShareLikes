using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImageShareLikes.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImagePathtoFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Images",
                newName: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Images",
                newName: "ImagePath");
        }
    }
}
