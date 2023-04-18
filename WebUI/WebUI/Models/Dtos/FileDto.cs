namespace WebUI.Models.Dtos
{
    public class FileDto
    {
        public Stream ContentStream { get; set; } = null!;
        public string ContentType { get; set; } = string.Empty;
        public string FileName { get; set; } = string.Empty;
    }
}
