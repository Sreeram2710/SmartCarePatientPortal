using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCarePatientPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddTestNameToMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestName",
                table: "MedicalRecords",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestName",
                table: "MedicalRecords");
        }
    }
}
