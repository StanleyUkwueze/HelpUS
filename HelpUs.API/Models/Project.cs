namespace HelpUs.API.Models
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal AmountRaised { get; set; }
        public int DonationCount { get; set; }
        public List<Donation> Donations { get; set; } = [];
        public string Image { get; set; }
        public string? CreatedBy { get; set; }
    }
}
