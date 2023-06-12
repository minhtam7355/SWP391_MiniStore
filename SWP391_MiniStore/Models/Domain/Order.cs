using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Order
    {
        [Key]
        public Guid OrderID { get; set; }

        public Guid? FkCustomerID { get; set; } // FK
        [ForeignKey("FkCustomerID")]
        public virtual Customer? Customer { get; set; } // (0,many) -> (1,1) NavigationProp 

        public Guid? FkStaffID { get; set; } // FK
        [ForeignKey("FkStaffID")]
        public virtual StoreStaff? Staff { get; set; } // (0,many) -> (0,1) NavigationProp

        public Guid? FkVoucherID { get; set; } // FK
        [ForeignKey("FkVoucherID")]
        public virtual Voucher? Voucher { get; set; } // (0,many) -> (0,1) NavigationProp

        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? OrderTotalBeforeVoucher { get; set; }
        public decimal? OrderTotalAfterVoucher { get; set; }
        public bool? IsDelivered { get; set; }
        public bool? OrderApprovalStatus { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; } // (1,1) -> (1,many) NavigationProp

    }
}
