using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class Payroll
    {
        [Key]
        public Guid PayrollID { get; set; }

        public Guid FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public StoreStaff Staff { get; set; } // (0,many) -> (1,1) NavigationProp

        public DateTime PayDate { get; set; }
        public string Payslip { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
    }
}
