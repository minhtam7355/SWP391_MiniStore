using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class WorkShift
    {
        [Key]
        public Guid ShiftID { get; set; }
        public string? StaffRole { get; set; }
        public bool? Holiday { get; set; }
        public string? DayOfWeek { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? WorkHours { get; set; }
        public decimal? SalaryCoef { get; set; }

        [InverseProperty("Shift")]
        public virtual ICollection<ShiftAssignment>? ShiftAssignments { get; set; } // (1,1) -> (0,many) NavigationProp
    }
}
