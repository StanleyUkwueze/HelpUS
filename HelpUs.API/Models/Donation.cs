namespace HelpUs.API.Models
{
    public class Donation: BaseEntity
    {
        public decimal Amount { get; set; }
        public string DonorName { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        public bool IsPaid { get; set; }
        public User User { get; set; }
        public Project Project { get; set; } = new();
    }
}
