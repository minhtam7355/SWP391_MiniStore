using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SWP391_MiniStore.Models.Domain
{
    public class Attendance
    {
        [Key]
        public Guid AttendanceID { get; set; }

        public Guid? FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public StoreStaff? Staff { get; set; } // (0,many) -> (1,1) NavigationProp

        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public DateTime? Date { get; set; }
    }
}
