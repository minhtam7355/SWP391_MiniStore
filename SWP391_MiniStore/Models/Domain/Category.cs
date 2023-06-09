using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Category
    {
        [Key]
        public Guid CategoryID { get; set; }
        public string? CategoryName { get; set; }

        [InverseProperty("Category")]
        public virtual ICollection<Product>? Products { get; set; } // (1,1) -> (1,many) NavigationProp

    }
}
