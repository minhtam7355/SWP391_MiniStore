using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using SWP391_MiniStore.Models.Domain;

namespace SWP391_MiniStore.Data
{
    public class MiniStoreDbContext : DbContext
    {
        public MiniStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StoreStaff> StoreStaff { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }
        public DbSet<ShiftAssignment> ShiftAssignments { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreStaff>().ToTable("tblStoreStaff");
            modelBuilder.Entity<WorkShift>().ToTable("tblWorkShifts");
            modelBuilder.Entity<ShiftAssignment>().ToTable("tblShiftAssignments");
            modelBuilder.Entity<Attendance>().ToTable("tblAttendances");
            modelBuilder.Entity<LeaveApplication>().ToTable("tblLeaveApplications");
            modelBuilder.Entity<Payroll>().ToTable("tblPayrolls");

            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.Staff)
                .WithMany(s => s.ShiftAssignments)
                .HasForeignKey(sa => sa.FkStaffID);

            modelBuilder.Entity<ShiftAssignment>()
                .HasOne(sa => sa.Shift)
                .WithMany(s => s.ShiftAssignments)
                .HasForeignKey(sa => sa.FkShiftID);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Staff)
                .WithMany(s => s.Attendances)
                .HasForeignKey(a => a.FkStaffID);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Staff)
                .WithMany(s => s.LeaveApplications)
                .HasForeignKey(la => la.FkStaffID);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Staff)
                .WithMany(s => s.Payrolls)
                .HasForeignKey(p => p.FkStaffID);

            // Specify precision and scale
            modelBuilder.Entity<StoreStaff>()
                .Property(ss => ss.HourlyRate)
                .HasPrecision(10, 2);

            modelBuilder.Entity<WorkShift>()
                .Property(ws => ws.SalaryCoef)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payroll>()
                .Property(p => p.GrossPay)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Payroll>()
                .Property(p => p.NetPay)
                .HasPrecision(10, 2);

        }
    }
}



//// Use this in migration

//// Insert rows for Sales Shift
//migrationBuilder.InsertData(
//    table: "tblWorkShifts",
//    columns: new[] { "ShiftID", "StaffRole", "Holiday", "DayOfWeek", "StartTime", "EndTime", "WorkHours", "SalaryCoef" },
//    values: new object[,]
//    {
//                    { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("12:00"), 6, 1 },
//                    { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("12:00"), TimeSpan.Parse("18:00"), 6, 1 },
//                    { Guid.NewGuid(), "Sales", false, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 },
//                    { Guid.NewGuid(), "Sales", false, "Sunday", TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 2 },
//                    { Guid.NewGuid(), "Sales", true, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 3 }
//    });

//// Insert rows for Guard Shift
//migrationBuilder.InsertData(
//    table: "tblWorkShifts",
//    columns: new[] { "ShiftID", "StaffRole", "Holiday", "DayOfWeek", "StartTime", "EndTime", "WorkHours", "SalaryCoef" },
//    values: new object[,]
//    {
//                    { Guid.NewGuid(), "Guard", false, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("18:00"), 12, 1 },
//                    { Guid.NewGuid(), "Guard", false, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 }
//    });

//// Insert rows for Staff
//migrationBuilder.InsertData(
//    table: "tblStoreStaff",
//    columns: new[] { "StaffID", "Username", "Email", "PhoneNumber", "Password", "Dob", "Address", "StaffRole", "HourlyRate", "StaffStatus" },
//    values: new object[,]
//    {
//                    // Manager
//                    { Guid.NewGuid(), "manager1", "manager1@gmail.com", "1234567890", "Password1#", new DateTime(1990, 1, 13), "Address 1", "Manager", new Random().Next(50, 71), true },
//                    { Guid.NewGuid(), "manager2", "manager2@gmail.com", "9876543210", "Password2#", new DateTime(1990, 2, 14), "Address 2", "Manager", new Random().Next(50, 71), true },
//                    { Guid.NewGuid(), "manager3", "manager3@gmail.com", "1111111111", "Password3#", new DateTime(1990, 3, 15), "Address 3", "Manager", new Random().Next(50, 71), true },

//                    // Sales
//                    { Guid.NewGuid(), "sales1", "sales1@gmail.com", "2222222222", "Password4#", new DateTime(1990, 4, 16), "Address 4", "Sales", new Random().Next(30, 51), true },
//                    { Guid.NewGuid(), "sales2", "sales2@gmail.com", "3333333333", "Password5#", new DateTime(1990, 5, 17), "Address 5", "Sales", new Random().Next(30, 51), true },
//                    { Guid.NewGuid(), "sales3", "sales3@gmail.com", "4444444444", "Password6#", new DateTime(1990, 6, 18), "Address 6", "Sales", new Random().Next(30, 51), true },

//                    // Guard
//                    { Guid.NewGuid(), "guard1", "guard1@gmail.com", "5555555555", "Password7#", new DateTime(1990, 7, 19), "Address 7", "Guard", new Random().Next(15, 31), true },
//                    { Guid.NewGuid(), "guard2", "guard2@gmail.com", "6666666666", "Password8#", new DateTime(1990, 8, 20), "Address 8", "Guard", new Random().Next(15, 31), true },
//                    { Guid.NewGuid(), "guard3", "guard3@gmail.com", "7777777777", "Password9#", new DateTime(1990, 9, 21), "Address 9", "Guard", new Random().Next(15, 31), true }
//    });
