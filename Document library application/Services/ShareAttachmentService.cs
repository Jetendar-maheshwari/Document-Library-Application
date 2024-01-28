using Document_library_application.Models;
using Document_library_application.Repositories;

namespace Document_library_application.Services
{
    public class ShareAttachmentService
    {
        private readonly IShareAttachmentRepository _attachmentShareRepository;
        private readonly ILogger<ShareAttachmentService> _logger;

        public ShareAttachmentService(IShareAttachmentRepository attachmentShareRepository, ILogger<ShareAttachmentService> logger)
        {
            {
                _attachmentShareRepository = attachmentShareRepository;
                _logger = logger;
            }
        }

        public void AddShareAttachment(ShareAttachment shareAttachment)
        {
            try
            {
                _attachmentShareRepository.AddShareAttachment(shareAttachment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding attachment: {ex}");
                throw;
            }
        }

        public string GetSharedAttachment(int sharedID)
        {
            try
            {
                return _attachmentShareRepository.GetSharedAttachment(sharedID);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting attachments: {ex}");
                throw;
            }
        }
    }
}

