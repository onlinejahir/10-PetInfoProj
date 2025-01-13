using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetInfo.Database.Migrations
{
    /// <inheritdoc />
    public partial class AnimalTypeRelationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalTypes",
                columns: table => new
                {
                    AnimalTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalTypes", x => x.AnimalTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AnimalTypeId",
                table: "Pets",
                column: "AnimalTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AnimalTypes_AnimalTypeId",
                table: "Pets",
                column: "AnimalTypeId",
                principalTable: "AnimalTypes",
                principalColumn: "AnimalTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AnimalTypes_AnimalTypeId",
                table: "Pets");

            migrationBuilder.DropTable(
                name: "AnimalTypes");

            migrationBuilder.DropIndex(
                name: "IX_Pets_AnimalTypeId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Pets");
        }
    }
}
