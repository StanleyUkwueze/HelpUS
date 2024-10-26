

namespace HelpUs.Service.DataTransferObjects.Requests
{
    public class TransactionDto
    {
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public string? Email { get; set; }
        public string? ProjectId { get; set; }
    }
}
