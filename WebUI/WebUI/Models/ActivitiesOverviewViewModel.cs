namespace WebUI.Models
{
    public class ActivitiesOverviewViewModel
    {
        public string UserAction { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public DateTime TimeOfAction { get; set; }
    }
}