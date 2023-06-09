using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Voucher
    {
        [Key]
        public Guid VoucherID { get; set; }
        public string? Code { get; set; }
        public decimal? DiscountPercentage { get; set; }

        [InverseProperty("Voucher")]
        public virtual ICollection<Order>? Orders { get; set; } // (0,1) -> (0,many) NavigationProp

    }
}
