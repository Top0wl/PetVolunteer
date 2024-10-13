using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "volunteer_id",
                table: "pets",
                newName: "pet_id");

            migrationBuilder.RenameIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                newName: "ix_pets_pet_id");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_pet_id",
                table: "pets",
                column: "pet_id",
                principalTable: "volunteers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteers_pet_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "pet_id",
                table: "pets",
                newName: "volunteer_id");

            migrationBuilder.RenameIndex(
                name: "ix_pets_pet_id",
                table: "pets",
                newName: "ix_pets_volunteer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteers_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");
        }
    }
}
