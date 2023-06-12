using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Feedback
    {
        [Key]
        public Guid FeedbackID { get; set; }

        public Guid? FkCustomerID { get; set; } // FK
        [ForeignKey("FkCustomerID")]
        public virtual Customer? Customer { get; set; } // (0,many) -> (1,1) NavigationProp 

        public Guid? FkProductID { get; set; } // FK
        [ForeignKey("FkProductID")]
        public virtual Product? Product { get; set; } // (0,many) -> (1,1) NavigationProp

        public int? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
