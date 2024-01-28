using Document_library_application.Models;

namespace Document_library_application.Repositories
{
    public interface IShareAttachmentRepository
    {
        void AddShareAttachment(ShareAttachment shareAttachment);
        string GetSharedAttachment(int ID);
    }
}
