using Moq;
using Document_library_application.Repositories;
using Document_library_application.Models;
using Microsoft.Extensions.Logging;

namespace YourProjectName.Tests.RepositoryTests
{
    public class AttachmentRepositoryTest
    {
        [Test]
        public void AddAttachment_ValidAttachment_Success()
        {
            var mockLogger = new Mock<ILogger<AttachmentRepository>>();
            var attachment = new Attachment
            {
                ID = 1,
                FileName = "test.txt",
                FilePath = "test.txt",
                CreateTime = 123456789,
                DownloadCount = 0,
                MimeType = "text/plain"
            };

            var repository = new AttachmentRepository("your_connection_string", mockLogger.Object);

            repository.AddAttachment(attachment);
        
            var addedAttachment = repository.DeleteAttachment(attachment.ID);
            // we can also run other function for test

            Assert.NotNull(addedAttachment, "The attachment was successfully added to the repository.");


        }

        [Test]
        public void GetAllAttachments_ReturnsListOfAttachments()
        {
            var mockLogger = new Mock<ILogger<AttachmentRepository>>();
            var repository = new AttachmentRepository("your_connection_string", mockLogger.Object);

            var attachments = repository.GetAllAttachments();

            Assert.IsNotNull(attachments);
            Assert.IsInstanceOf<List<Attachment>>(attachments);
        }

        [Test]
        public async Task GetFilePath_ReturnsCorrectFilePath()
        {
            var mockLogger = new Mock<ILogger<AttachmentRepository>>();
            var repository = new AttachmentRepository("your_connection_string", mockLogger.Object);
            var fileName = "test.txt";
            var expectedFilePath = "/path/to/test.txt";

            var filePath = await repository.GetFilePath(fileName);

            Assert.That(filePath, Is.EqualTo(expectedFilePath));
        }


    }
}
