namespace WebUI.Models.ViewModels
{
    public class SelectDownloadViewModel
    {
        public List<GetUserEventForDownloadViewModel> UserEventsToDownload { get; set; } = new();
    }

    public class GetUserEventForDownloadViewModel : GetUserEventViewModel
    {
        public bool IsSelected { get; set; }
    }
}
