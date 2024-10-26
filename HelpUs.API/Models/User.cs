namespace HelpUs.API.Models
{
    public class User: BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PaswordHash { get; set; }
        public string Role { get; set; }
        public List<Donation> Donations { get; set; } = [];
    }
}
