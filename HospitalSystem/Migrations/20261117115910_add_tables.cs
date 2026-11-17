using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem.Migrations
{
    /// <inheritdoc />
    public partial class add_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    insurance = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    department_id = table.Column<int>(type: "int", nullable: false),
                    specialization_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.id);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_department_id",
                        column: x => x.department_id,
                        principalTable: "Departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_specialization_id",
                        column: x => x.specialization_id,
                        principalTable: "Specializations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    time = table.Column<TimeSpan>(type: "time", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<int>(type: "int", nullable: false),
                    patient_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medicine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doctor_id = table.Column<int>(type: "int", nullable: false),
                    patient_id = table.Column<int>(type: "int", nullable: false),
                    appointment_id = table.Column<int>(type: "int", nullable: false),
                    Appointmentid = table.Column<int>(type: "int", nullable: true),
                    Doctorid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Appointments_Appointmentid",
                        column: x => x.Appointmentid,
                        principalTable: "Appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Appointments_appointment_id",
                        column: x => x.appointment_id,
                        principalTable: "Appointments",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_Doctorid",
                        column: x => x.Doctorid,
                        principalTable: "Doctors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Doctors_doctor_id",
                        column: x => x.doctor_id,
                        principalTable: "Doctors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_doctor_id",
                table: "Appointments",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_patient_id",
                table: "Appointments",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_department_id",
                table: "Doctors",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_specialization_id",
                table: "Doctors",
                column: "specialization_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_appointment_id",
                table: "Prescriptions",
                column: "appointment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Appointmentid",
                table: "Prescriptions",
                column: "Appointmentid");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_doctor_id",
                table: "Prescriptions",
                column: "doctor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Doctorid",
                table: "Prescriptions",
                column: "Doctorid");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_patient_id",
                table: "Prescriptions",
                column: "patient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
