using Document_library_application.Models;
using Document_library_application.Repositories;

namespace Document_library_application.Services
{
    public class AttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly ILogger<AttachmentService> _logger;

        public AttachmentService(IAttachmentRepository attachmentRepository, ILogger<AttachmentService> logger)
        {
            _attachmentRepository = attachmentRepository;
            _logger = logger;
        }

        public void AddAttachment(Attachment attachment)
        {
            try
            {
                _attachmentRepository.AddAttachment(attachment);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding attachment: {ex}");
                throw;
            }
        }

        public List<Attachment> GetAllAttachments()
        {
            try
            {
                return _attachmentRepository.GetAllAttachments();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting attachments: {ex}");
                throw;
            }
        }

        public bool DeleteAttachment(int id)
        {
            try
            {
                return _attachmentRepository.DeleteAttachment(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while deleting attachment: {ex}");
                throw;
            }
        }

        public async Task<string> GetFilePath(string fileName)
        {
            return await _attachmentRepository.GetFilePath(fileName);
        }

        public void IncrementDownloadCount(string fileName)
        {
            _attachmentRepository.IncrementDownloadCount(fileName);
        }

    }
}
