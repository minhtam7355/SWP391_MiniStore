using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class Payroll
    {
        [Key]
        public Guid PayrollID { get; set; }

        public Guid? FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public virtual StoreStaff? Staff { get; set; } // (0,many) -> (1,1) NavigationProp

        public Guid? FkManagerID { get; set; } // FK to the manager who sent the payroll
        [ForeignKey("FkManagerID")]
        public virtual StoreStaff? Manager { get; set; } // (0,many) -> (1,1) NavigationProp

        public DateTime? PayDate { get; set; }
        public string? Payslip { get; set; }
        public decimal? GrossPay { get; set; }
        public decimal? NetPay { get; set; }
    }
}
