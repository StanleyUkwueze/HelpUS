using HelpUs.API.Models;

namespace HelpUs.API.DataTransferObjects.Responses
{
    public class ProjectResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal AmountRaised { get; set; }
        public int DonationCount { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string? CreatedBy { get; set; }
    }
}