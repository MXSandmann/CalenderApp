namespace WebUI.Models.Dtos
{
    public class SearchUserEventsDto
    {
        public string Entry { get; set; } = string.Empty;
        public int Limit { get; set; } = 5;
        public int Offset { get; set; } = 0;
        public IEnumerable<GetUserEventViewModel> UserEvents { get; set; } = null!;
    }
}
