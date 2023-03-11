namespace ApplicationCore.Models.Dtos
{
    public class UserActivityRecordDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserAction { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public DateTime TimeOfAction { get; set; }
    }
}
