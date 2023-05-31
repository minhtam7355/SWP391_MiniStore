using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWP391_MiniStore.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblStoreStaff",
                columns: table => new
                {
                    StaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HourlyRate = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    StaffStatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStoreStaff", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "tblWorkShifts",
                columns: table => new
                {
                    ShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StaffRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Holiday = table.Column<bool>(type: "bit", nullable: true),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    WorkHours = table.Column<int>(type: "int", nullable: true),
                    SalaryCoef = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWorkShifts", x => x.ShiftID);
                });

            migrationBuilder.CreateTable(
                name: "tblAttendances",
                columns: table => new
                {
                    AttendanceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CheckIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOut = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAttendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_tblAttendances_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblLeaveApplications",
                columns: table => new
                {
                    LeaveApplicationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeaveStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LeaveReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveApprovalStatus = table.Column<bool>(type: "bit", nullable: true),
                    LeaveApprovalReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLeaveApplications", x => x.LeaveApplicationID);
                    table.ForeignKey(
                        name: "FK_tblLeaveApplications_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblPayrolls",
                columns: table => new
                {
                    PayrollID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PayDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Payslip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GrossPay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    NetPay = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPayrolls", x => x.PayrollID);
                    table.ForeignKey(
                        name: "FK_tblPayrolls_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                });

            migrationBuilder.CreateTable(
                name: "tblShiftAssignments",
                columns: table => new
                {
                    AssignmentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkStaffID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FkShiftID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShiftAssignments", x => x.AssignmentID);
                    table.ForeignKey(
                        name: "FK_tblShiftAssignments_tblStoreStaff_FkStaffID",
                        column: x => x.FkStaffID,
                        principalTable: "tblStoreStaff",
                        principalColumn: "StaffID");
                    table.ForeignKey(
                        name: "FK_tblShiftAssignments_tblWorkShifts_FkShiftID",
                        column: x => x.FkShiftID,
                        principalTable: "tblWorkShifts",
                        principalColumn: "ShiftID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAttendances_FkStaffID",
                table: "tblAttendances",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblLeaveApplications_FkStaffID",
                table: "tblLeaveApplications",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPayrolls_FkStaffID",
                table: "tblPayrolls",
                column: "FkStaffID");

            migrationBuilder.CreateIndex(
                name: "IX_tblShiftAssignments_FkShiftID",
                table: "tblShiftAssignments",
                column: "FkShiftID");

            migrationBuilder.CreateIndex(
                name: "IX_tblShiftAssignments_FkStaffID",
                table: "tblShiftAssignments",
                column: "FkStaffID");


            // Use this in migration

            // Insert rows for Sale Shift
            migrationBuilder.InsertData(
                table: "tblWorkShifts",
                columns: new[] { "ShiftID", "StaffRole", "Holiday", "DayOfWeek", "StartTime", "EndTime", "WorkHours", "SalaryCoef" },
                values: new object[,]
                {
                                { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("12:00"), 6, 1 },
                                { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("12:00"), TimeSpan.Parse("18:00"), 6, 1 },
                                { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 },
                                { Guid.NewGuid(), "Sales", false, "Sunday", TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 2 },
                                { Guid.NewGuid(), "Sales", true, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 3 }
                });

            // Insert rows for Guard Shift
            migrationBuilder.InsertData(
                table: "tblWorkShifts",
                columns: new[] { "ShiftID", "StaffRole", "Holiday", "DayOfWeek", "StartTime", "EndTime", "WorkHours", "SalaryCoef" },
                values: new object[,]
                {
                                { Guid.NewGuid(), "Guard", false, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("18:00"), 12, 1 },
                                { Guid.NewGuid(), "Guard", false, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 }
                });

            // Insert rows for Staff
            migrationBuilder.InsertData(
                table: "tblStoreStaff",
                columns: new[] { "StaffID", "Username", "Email", "PhoneNumber", "Password", "Dob", "Address", "StaffRole", "HourlyRate", "StaffStatus" },
                values: new object[,]
                {
                                // Manager
                                { Guid.NewGuid(), "manager1", "manager1@gmail.com", "123456789", "Password1", new DateTime(1990, 1, 1), "Address 1", "Manager", new Random().Next(50, 71), true },
                                { Guid.NewGuid(), "manager2", "manager2@gmail.com", "987654321", "Password2", new DateTime(1990, 2, 2), "Address 2", "Manager", new Random().Next(50, 71), true },
                                { Guid.NewGuid(), "manager3", "manager3@gmail.com", "111111111", "Password3", new DateTime(1990, 3, 3), "Address 3", "Manager", new Random().Next(50, 71), true },

                                // Sale
                                { Guid.NewGuid(), "sales1", "sales1@gmail.com", "222222222", "Password4", new DateTime(1990, 4, 4), "Address 4", "Sales", new Random().Next(30, 51), true },
                                { Guid.NewGuid(), "sales2", "sales2@gmail.com", "333333333", "Password5", new DateTime(1990, 5, 5), "Address 5", "Sales", new Random().Next(30, 51), true },
                                { Guid.NewGuid(), "sales3", "sales3@gmail.com", "444444444", "Password6", new DateTime(1990, 6, 6), "Address 6", "Sales", new Random().Next(30, 51), true },

                                // Guard
                                { Guid.NewGuid(), "guard1", "guard1@gmail.com", "555555555", "Password7", new DateTime(1990, 7, 7), "Address 7", "Guard", new Random().Next(15, 31), true },
                                { Guid.NewGuid(), "guard2", "guard2@gmail.com", "666666666", "Password8", new DateTime(1990, 8, 8), "Address 8", "Guard", new Random().Next(15, 31), true },
                                { Guid.NewGuid(), "guard3", "guard3@gmail.com", "777777777", "Password9", new DateTime(1990, 9, 9), "Address 9", "Guard", new Random().Next(15, 31), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAttendances");

            migrationBuilder.DropTable(
                name: "tblLeaveApplications");

            migrationBuilder.DropTable(
                name: "tblPayrolls");

            migrationBuilder.DropTable(
                name: "tblShiftAssignments");

            migrationBuilder.DropTable(
                name: "tblStoreStaff");

            migrationBuilder.DropTable(
                name: "tblWorkShifts");
        }
    }
}
