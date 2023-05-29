using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class LeaveApplication
    {
        [Key]
        public Guid LeaveApplicationID { get; set; }

        public Guid FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public StoreStaff Staff { get; set; } // (0,many) -> (1,1) NavigationProp

        public DateOnly LeaveStart { get; set; }
        public DateOnly LeaveEnd { get; set; }
        public string? LeaveReason { get; set; }
        public bool? LeaveApprovalStatus { get; set; }
        public string? LeaveApprovalReason { get; set; }
    }
}
