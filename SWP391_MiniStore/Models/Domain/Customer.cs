using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWP391_MiniStore.Models.Domain
{
    public class Customer
    {
        [Key]
        public Guid CustomerID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public decimal? Balance { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<Order>? Orders { get; set; } // (1,1) -> (0,many) NavigationProp
        
        [InverseProperty("Customer")]
        public virtual ICollection<Feedback>? Feedbacks { get; set; } // (1,1) -> (0,many) NavigationProp
    }
}
