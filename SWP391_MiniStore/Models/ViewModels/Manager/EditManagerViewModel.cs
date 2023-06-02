using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SWP391_MiniStore.Models.ViewModels.Manager
{
    public class EditManagerViewModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
    }
}
