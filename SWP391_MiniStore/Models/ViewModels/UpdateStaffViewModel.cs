namespace SWP391_MiniStore.Models.ViewModels
{
    public class UpdateStaffViewModel
    {
        public Guid StaffID { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? StaffRole { get; set; }
        public decimal? HourlyRate { get; set; }
        public bool? StaffStatus { get; set; }
    }
}
