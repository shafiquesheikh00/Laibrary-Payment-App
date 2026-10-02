using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Laibrary_Payment_App.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipAndSeatManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentMembership_SeatId",
                table: "StudentMembership");

            migrationBuilder.DropIndex(
                name: "IX_StudentMembership_StudentId",
                table: "StudentMembership");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "StudentMembership",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_Seat_Status",
                table: "StudentMembership",
                columns: new[] { "SeatId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_Student_Status",
                table: "StudentMembership",
                columns: new[] { "StudentId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPlan_PlanName",
                table: "MembershipPlan",
                column: "PlanName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentMembership_Seat_Status",
                table: "StudentMembership");

            migrationBuilder.DropIndex(
                name: "IX_StudentMembership_Student_Status",
                table: "StudentMembership");

            migrationBuilder.DropIndex(
                name: "IX_MembershipPlan_PlanName",
                table: "MembershipPlan");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "StudentMembership");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_SeatId",
                table: "StudentMembership",
                column: "SeatId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMembership_StudentId",
                table: "StudentMembership",
                column: "StudentId");
        }
    }
}
