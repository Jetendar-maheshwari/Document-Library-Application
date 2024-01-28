using System;
namespace Document_library_application.Models
{
    public class ShareAttachment
    {
        public int ID { get; set; }
        public required int AttachmentID { get; set; }
        public required int SharedID { get; set; }
        public required int CreateTime { get; set; }
        public required int ExpireTime { get; set; }
    }
}

