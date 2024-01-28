using System;
namespace Document_library_application.Models
{
    public class Attachment
    {
        public int ID { get; set; }
        public required string FileName { get; set; }
        public int CreateTime { get; set; }
        public required string FilePath { get; set; }
        public string? MimeType { get; set; }
        public int DownloadCount { get; set; }
        public string? Role { get; set; }
        public string? Meta { get; set; }
    }
}