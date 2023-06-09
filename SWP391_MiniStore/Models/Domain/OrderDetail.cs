using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class OrderDetail
    {
        [Key]
        public Guid? FkOrderID { get; set; } // FK  
        [ForeignKey("FkOrderID")]
        public virtual Order? Order { get; set; } // (1,many) -> (1,1) NavigationProp 

        [Key]
        public Guid? FkProductID { get; set; } // FK
        [ForeignKey("FkProductID")]
        public virtual Product? Product { get; set; } // (0,many) -> (1,1) NavigationProp

        public decimal? OrderedProductPrice { get; set; }
        public int? OrderedProductQuantity { get; set; }
        public decimal? SubTotal { get; set; }
    }
}
