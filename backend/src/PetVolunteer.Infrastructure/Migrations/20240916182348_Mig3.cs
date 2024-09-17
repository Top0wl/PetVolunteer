using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "volunteers",
                newName: "requisites");

            migrationBuilder.RenameColumn(
                name: "phone_number_value",
                table: "volunteers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "full_name_patronymic",
                table: "volunteers",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "full_name_last_name",
                table: "volunteers",
                newName: "lastname");

            migrationBuilder.RenameColumn(
                name: "full_name_first_name",
                table: "volunteers",
                newName: "firstname");

            migrationBuilder.RenameColumn(
                name: "experience_value",
                table: "volunteers",
                newName: "experience");

            migrationBuilder.RenameColumn(
                name: "email_value",
                table: "volunteers",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "SocialMedia",
                table: "volunteers",
                newName: "social_media");

            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "pets",
                newName: "requisites");

            migrationBuilder.RenameColumn(
                name: "Photos",
                table: "pets",
                newName: "photos");

            migrationBuilder.RenameColumn(
                name: "NumberHouse",
                table: "pets",
                newName: "number_house");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "volunteers",
                newName: "Requisites");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "volunteers",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "volunteers",
                newName: "full_name_patronymic");

            migrationBuilder.RenameColumn(
                name: "lastname",
                table: "volunteers",
                newName: "full_name_last_name");

            migrationBuilder.RenameColumn(
                name: "firstname",
                table: "volunteers",
                newName: "full_name_first_name");

            migrationBuilder.RenameColumn(
                name: "experience",
                table: "volunteers",
                newName: "experience_value");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "volunteers",
                newName: "email_value");

            migrationBuilder.RenameColumn(
                name: "social_media",
                table: "volunteers",
                newName: "SocialMediaList");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "pets",
                newName: "Requisites");

            migrationBuilder.RenameColumn(
                name: "photos",
                table: "pets",
                newName: "Photos");

            migrationBuilder.RenameColumn(
                name: "number_house",
                table: "pets",
                newName: "NumberHouse");
        }
    }
}
