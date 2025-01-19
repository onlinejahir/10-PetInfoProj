using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetInfo.Database.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionFileAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionFileName",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionFileName",
                table: "Pets");
        }
    }
}
