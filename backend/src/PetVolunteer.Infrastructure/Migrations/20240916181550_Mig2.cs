using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetVolunteer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "volunteers");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "volunteers",
                newName: "full_name_last_name");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "volunteers",
                newName: "full_name_first_name");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "volunteers",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "volunteers",
                newName: "email_value");

            migrationBuilder.RenameColumn(
                name: "experience",
                table: "volunteers",
                newName: "experience_value");

            migrationBuilder.RenameColumn(
                name: "pet_type",
                table: "pets",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "health_information",
                table: "pets",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "pets",
                newName: "NumberHouse");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "volunteers",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "full_name_first_name",
                table: "volunteers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "full_name_patronymic",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "additional_health_information",
                table: "pets",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "animal_type",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_name_patronymic",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "additional_health_information",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "animal_type",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "full_name_last_name",
                table: "volunteers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "full_name_first_name",
                table: "volunteers",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "phone_number_value",
                table: "volunteers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "experience_value",
                table: "volunteers",
                newName: "experience");

            migrationBuilder.RenameColumn(
                name: "email_value",
                table: "volunteers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "pets",
                newName: "pet_type");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "pets",
                newName: "health_information");

            migrationBuilder.RenameColumn(
                name: "NumberHouse",
                table: "pets",
                newName: "address");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "volunteers",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
