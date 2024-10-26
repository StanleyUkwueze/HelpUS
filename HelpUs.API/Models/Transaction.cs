using HelpUs.API.Models;

namespace HelpUs.API.Entity.Entities
{
    public class Transaction:BaseEntity
    {
        public string CustomerName { get; set; }
        public int Amount { get; set; }
        public string TrxRef { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ProjectId { get; set; }
        public bool Status { get; set; }
    }
}
