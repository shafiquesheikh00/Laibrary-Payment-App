using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Laibrary_Payment_App.Migrations
{
    /// <inheritdoc />
    public partial class InitialLibraryDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MembershipPlan",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationValue = table.Column<int>(type: "int", nullable: false),
                    DurationUnit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlan", x => x.PlanId);
                });

            migrationBuilder.CreateTable(
                name: "SeatMaster",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Available"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatMaster", x => x.SeatId);
                });

            migrationBuilder.CreateTable(
                name: "StudentMaster",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CollegeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMaster", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "StudentMembership",
                columns: table => new
                {
                    MembershipId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0.00m),
                    FinalFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Active"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMembership", x => x.MembershipId);
                    table.ForeignKey(
                        name: "FK_StudentMembership_MembershipPlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlan",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentMembership_SeatMaster_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SeatMaster",
                        principalColumn: "SeatId",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_StudentMembership_StudentMaster_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentMaster",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MembershipPlan",
                columns: new[] { "PlanId", "CreatedDate", "DurationUnit", "DurationValue", "Fee", "IsActive", "ModifiedDate", "PlanName" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Day", 1, 50.00m, true, null, "Daily" },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Week", 1, 250.00m, true, null, "Weekly" },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Month", 1, 800.00m, true, null, "Monthly" },
                    { 4, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Month", 3, 2200.00m, true, null, "Quarterly" },
                    { 5, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Year", 1, 8000.00m, true, null, "Yearly" }
                });

            migrationBuilder.InsertData(
                table: "SeatMaster",
                columns: new[] { "SeatId", "Area", "CreatedDate", "IsActive", "ModifiedDate", "SeatNumber", "Status" },
                values: new object[,]
                {
                    { 1, "Main Study Hall", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "A01", "Available" },
                    { 2, "Main Study Hall", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "A02", "Available" },
                    { 3, "Main Study Hall", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "A03", "Available" },
                    { 4, "Main Study Hall", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "A04", "Available" },
                    { 5, "Main Study Hall", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), true, null, "A05", "Available" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatMaster_SeatNumber",
                table: "SeatMaster",
                column: "SeatNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaster_MobileNo",
                table: "StudentMaster",
                column: "MobileNo");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaster_StudentCode",
                table: "StudentMaster",
                column: "StudentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_EndDate",
                table: "StudentMembership",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_PlanId",
                table: "StudentMembership",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_SeatId",
                table: "StudentMembership",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_StartDate",
                table: "StudentMembership",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_Status",
                table: "StudentMembership",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_StudentId",
                table: "StudentMembership",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentMembership");

            migrationBuilder.DropTable(
                name: "MembershipPlan");

            migrationBuilder.DropTable(
                name: "SeatMaster");

            migrationBuilder.DropTable(
                name: "StudentMaster");
        }
    }
}
