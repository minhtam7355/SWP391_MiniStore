using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class ShiftAssignment
    {
        [Key]
        public Guid AssignmentID { get; set; }

        public Guid? FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public virtual StoreStaff? Staff { get; set; } // (0,many) -> (1,1) NavigationProp 

        public Guid? FkShiftID { get; set; } // FK
        [ForeignKey("FkShiftID")]
        public virtual WorkShift? Shift { get; set; } // (0,many) -> (1,1) NavigationProp 

        public DateTime? Date { get; set; }
        public string? DayOfWeek { get; set; }
    }
}
