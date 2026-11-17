using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class deedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("\r\nINSERT INTO Departments ( name) VALUES\r\n( 'Cardiology'),\r\n( 'Pediatrics'),\r\n( 'Orthopedics'),\r\n( 'Neurology'),\r\n( 'Dermatology');\r\n\r\n\r\nINSERT INTO Specializations ( name) VALUES\r\n('Consultant Cardiologist'),\r\n( 'Pediatrician'),\r\n( 'Orthopedic Surgeon'),\r\n( 'Neurologist'),\r\n( 'Dermatologist');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
