namespace HelpUs.API.DataTransferObjects.Requests
{
    public class ProjectCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal TargetAmount { get; set; }
    }
}