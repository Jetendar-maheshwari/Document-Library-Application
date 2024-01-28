using Document_library_application.Models;

namespace Document_library_application.Repositories
{
    public interface IAttachmentRepository
    {
        void AddAttachment(Attachment attachment);
        bool DeleteAttachment(int id);
        List<Attachment> GetAllAttachments();
        Task<string> GetFilePath(string fileName);
        void IncrementDownloadCount(string fileName);
    }
}
