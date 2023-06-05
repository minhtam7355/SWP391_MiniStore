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
            
            modelBuilder.Entity<LeaveApplication>()
                .HasOne(la => la.Manager)
                .WithMany(m => m.ProcessedLeaveApplications)
                .HasForeignKey(la => la.FkManagerID)
                .IsRequired(false);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Staff)
                .WithMany(s => s.Payrolls)
                .HasForeignKey(p => p.FkStaffID);

            modelBuilder.Entity<Payroll>()
                .HasOne(p => p.Manager)
                .WithMany(m => m.SentPayrolls)
                .HasForeignKey(p => p.FkManagerID);

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

            // Insert data
            // Insert rows for Sales Shift
            modelBuilder.Entity<WorkShift>().HasData(
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("06:00"), EndTime = TimeSpan.Parse("12:00"), WorkHours = 6, SalaryCoef = 1 },
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("12:00"), EndTime = TimeSpan.Parse("18:00"), WorkHours = 6, SalaryCoef = 1 },
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = (decimal)1.5 },
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = false, DayOfWeek = "Sunday", StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = 2 },
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Sales", Holiday = true, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = 3 }
            );

            // Insert rows for Guard Shift
            modelBuilder.Entity<WorkShift>().HasData(
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Guard", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("06:00"), EndTime = TimeSpan.Parse("18:00"), WorkHours = 12, SalaryCoef = 1 },
                new WorkShift { ShiftID = Guid.NewGuid(), StaffRole = "Guard", Holiday = false, DayOfWeek = null, StartTime = TimeSpan.Parse("18:00"), EndTime = TimeSpan.Parse("06:00"), WorkHours = 12, SalaryCoef = (decimal)1.5 }
            );

            // Insert rows for Staff
            modelBuilder.Entity<StoreStaff>().HasData(
                // Manager
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager1", Email = "manager1@gmail.com", PhoneNumber = "1234567890", Password = "Password1#", Dob = new DateTime(1990, 1, 13), Address = "Address 1", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager2", Email = "manager2@gmail.com", PhoneNumber = "9876543210", Password = "Password2#", Dob = new DateTime(1990, 2, 14), Address = "Address 2", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "manager3", Email = "manager3@gmail.com", PhoneNumber = "1111111111", Password = "Password3#", Dob = new DateTime(1990, 3, 15), Address = "Address 3", StaffRole = "Manager", HourlyRate = new Random().Next(50, 71), StaffStatus = true },

                // Sales
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales1", Email = "sales1@gmail.com", PhoneNumber = "2222222222", Password = "Password4#", Dob = new DateTime(1990, 4, 16), Address = "Address 4", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales2", Email = "sales2@gmail.com", PhoneNumber = "3333333333", Password = "Password5#", Dob = new DateTime(1990, 5, 17), Address = "Address 5", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "sales3", Email = "sales3@gmail.com", PhoneNumber = "4444444444", Password = "Password6#", Dob = new DateTime(1990, 6, 18), Address = "Address 6", StaffRole = "Sales", HourlyRate = new Random().Next(30, 51), StaffStatus = true },

                // Guard
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard1", Email = "guard1@gmail.com", PhoneNumber = "5555555555", Password = "Password7#", Dob = new DateTime(1990, 7, 19), Address = "Address 7", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard2", Email = "guard2@gmail.com", PhoneNumber = "6666666666", Password = "Password8#", Dob = new DateTime(1990, 8, 20), Address = "Address 8", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true },
                new StoreStaff { StaffID = Guid.NewGuid(), Username = "guard3", Email = "guard3@gmail.com", PhoneNumber = "7777777777", Password = "Password9#", Dob = new DateTime(1990, 9, 21), Address = "Address 9", StaffRole = "Guard", HourlyRate = new Random().Next(15, 31), StaffStatus = true }
            );
        }
    }
}
