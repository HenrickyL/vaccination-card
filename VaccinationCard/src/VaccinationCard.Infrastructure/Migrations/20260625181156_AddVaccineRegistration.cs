using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VaccinationCard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVaccineRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VaccineRegistrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    VaccineId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Dose = table.Column<int>(type: "integer", nullable: false),
                    Lot = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccineRegistrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccineRegistrations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VaccineRegistrations_Vaccines_VaccineId",
                        column: x => x.VaccineId,
                        principalTable: "Vaccines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRegistrations_ApplicationDate",
                table: "VaccineRegistrations",
                column: "ApplicationDate");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRegistrations_Dose",
                table: "VaccineRegistrations",
                column: "Dose");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRegistrations_PatientId",
                table: "VaccineRegistrations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_VaccineRegistrations_VaccineId",
                table: "VaccineRegistrations",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "Registrations_PatientVaccineDose_Unique",
                table: "VaccineRegistrations",
                columns: new[] { "PatientId", "VaccineId", "Dose" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VaccineRegistrations");
        }
    }
}
