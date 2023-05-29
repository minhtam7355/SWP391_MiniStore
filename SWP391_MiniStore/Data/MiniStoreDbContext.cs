using Microsoft.EntityFrameworkCore;
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
        }
    }
}



// Use this in migration
//// Insert rows for Sale Employee
//migrationBuilder.InsertData(
//    table: "tblWorkShifts",
//    columns: new[] { "ShiftID", "employeeRole", "holiday", "dayOfWeek", "startTime", "endTime", "workHours", "salaryCoef" },
//    values: new object[,]
//    {
//            { Guid.NewGuid(), "Sale", 0, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("12:00"), 6, 1 },
//            { Guid.NewGuid(), "Sale", 0, null, TimeSpan.Parse("12:00"), TimeSpan.Parse("18:00"), 6, 1 },
//            { Guid.NewGuid(), "Sale", 0, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 },
//            { Guid.NewGuid(), "Sale", 0, "Sunday", TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 2 },
//            { Guid.NewGuid(), "Sale", 1, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 3 }
//    });

//// Insert rows for Guard Employee
//migrationBuilder.InsertData(
//    table: "tblWorkShifts",
//    columns: new[] { "ShiftID", "employeeRole", "holiday", "dayOfWeek", "startTime", "endTime", "workHours", "salaryCoef" },
//    values: new object[,]
//    {
//            { Guid.NewGuid(), "Guard", 0, null, TimeSpan.Parse("06:00"), TimeSpan.Parse("18:00"), 12, 1 },
//            { Guid.NewGuid(), "Guard", 0, null, TimeSpan.Parse("18:00"), TimeSpan.Parse("06:00"), 12, 1.5 }
//    });