using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Volunteers",
                newName: "volunteers");

            migrationBuilder.RenameTable(
                name: "Pets",
                newName: "pets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "volunteers",
                newName: "Volunteers");

            migrationBuilder.RenameTable(
                name: "pets",
                newName: "Pets");
        }
    }
}
