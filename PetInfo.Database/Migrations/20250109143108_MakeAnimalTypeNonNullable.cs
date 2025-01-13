using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetInfo.Database.Migrations
{
    /// <inheritdoc />
    public partial class MakeAnimalTypeNonNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalTypeID",
                table: "AnimalTypes",
                newName: "AnimalTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalTypeId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalTypeId",
                table: "AnimalTypes",
                newName: "AnimalTypeID");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalTypeId",
                table: "Pets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
