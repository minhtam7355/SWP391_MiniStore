using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class LeaveApplication
    {
        [Key]
        public Guid LeaveApplicationID { get; set; }

        public Guid? FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public virtual StoreStaff? Staff { get; set; } // (0,many) -> (1,1) NavigationProp

        public Guid? FkManagerID { get; set; } // FK to the manager who approves or denies the leave application
        [ForeignKey("FkManagerID")]
        public virtual StoreStaff? Manager { get; set; } // (0,many) -> (0,1) NavigationProp

        public DateTime? LeaveStart { get; set; }
        public DateTime? LeaveEnd { get; set; }
        public string? LeaveReason { get; set; }
        public bool? LeaveApprovalStatus { get; set; }
        public string? LeaveApprovalReason { get; set; }
    }
}
