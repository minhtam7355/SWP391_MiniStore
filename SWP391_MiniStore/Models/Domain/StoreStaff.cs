using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class StoreStaff
    {
        [Key]
        public Guid StaffID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? StaffRole { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? StaffStatus { get; set; }

        [InverseProperty("Staff")]
        public virtual ICollection<ShiftAssignment>? ShiftAssignments { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Staff")]
        public virtual ICollection<Attendance>? Attendances { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Staff")]
        public virtual ICollection<LeaveApplication>? LeaveApplications { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Manager")]
        public virtual ICollection<LeaveApplication>? ProcessedLeaveApplications { get; set; } // (0,1) -> (0,many) NavigationProp

        [InverseProperty("Staff")]
        public virtual ICollection<Payroll>? Payrolls { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Manager")]
        public virtual ICollection<Payroll>? SentPayrolls { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Staff")]
        public virtual ICollection<Order>? Orders { get; set; } // (0,1) -> (0,many) NavigationProp
    }
}
