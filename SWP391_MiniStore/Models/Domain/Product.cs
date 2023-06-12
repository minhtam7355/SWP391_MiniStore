using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Product
    {
        [Key]
        public Guid ProductID { get; set; }

        public Guid? FkCategoryID { get; set; } // FK
        [ForeignKey("FkCategoryID")]
        public virtual Category? Category { get; set; } // (1,many) -> (1,1) NavigationProp 

        public string? ProductName { get; set; }
        public string? ProductImg { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductBrand { get; set; }
        public decimal? ProductPrice { get; set; }
        public int? ProductQuantity { get; set; }
        public bool? ProductStatus { get; set; }

        [InverseProperty("Product")]
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; } // (1,1) -> (0,many) NavigationProp

        [InverseProperty("Product")]
        public virtual ICollection<Feedback>? Feedbacks { get; set; } // (1,1) -> (0,many) NavigationProp
    }
}
