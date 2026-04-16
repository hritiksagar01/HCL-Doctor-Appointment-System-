using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Doctor_Appointment_System.Migrations
{
    /// <inheritdoc />
    public partial class AddClinicAddressToDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClinicAddress",
                table: "Doctors",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClinicAddress",
                table: "Doctors");
        }
    }
}
