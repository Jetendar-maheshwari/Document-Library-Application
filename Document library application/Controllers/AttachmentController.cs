using Microsoft.AspNetCore.Mvc;
using Document_library_application.Models;
using Document_library_application.Services;
using Document_library_application.Helpers;

namespace Document_library_application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly ILogger<AttachmentController> _logger;
        private readonly AttachmentService _attachmentService;
        private readonly ShareAttachmentService _shareAttachmentService;
        private static readonly Random random = new Random();

        public AttachmentController(ILogger<AttachmentController> logger, AttachmentService attachmentService, ShareAttachmentService shareAttachmentService)
        {
            _logger = logger;
            _attachmentService = attachmentService;
            _shareAttachmentService = shareAttachmentService;
        }

        // Task #1: Fetch all available attachment from database
        [HttpGet("get")]
        public IActionResult GetAllAttachments()
        {
            try
            {
                var attachments = _attachmentService.GetAllAttachments();
                return Ok(attachments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching attachments: {ex}");
                return StatusCode(500, new { Message = "Error occurred while fetching attachments." });
            }
        }

        // Task #2: Upload the attachment and save in the database
        [HttpPost("add")]
        public IActionResult AddAttachment([FromForm] IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = file.FileName;
                    string currentDirectory = Directory.GetCurrentDirectory();

                    string uploadDirectory = Path.Combine(currentDirectory, "upload_attachments");

                    if (!Directory.Exists(uploadDirectory))
                    {
                        Directory.CreateDirectory(uploadDirectory);
                    }

                    int unixTimestamp = AttachmentHelper.GetCurrentUnixTimestamp();

                    int randomNumber = random.Next(100000, 999999);
                    string filePath = Path.Combine(uploadDirectory, randomNumber + "-" + fileName);


                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                   
                    string mimeType = AttachmentHelper.GetMimeType(fileName);

                    Attachment attachment = new Attachment
                    {
                        FileName = fileName,
                        FilePath = randomNumber + "-" + fileName,
                        CreateTime = unixTimestamp,
                        DownloadCount = 0,
                        MimeType = mimeType,
                    };

                    _attachmentService.AddAttachment(attachment);

                    return Ok(new { Message = "File uploaded successfully!" });
                }
                else
                {
                    return BadRequest("Invalid file");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Task #3: Download the attachment and add the download count in the database
        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadAttachment(string fileName)
        {
            try
            {
                string filePath = await _attachmentService.GetFilePath(fileName);

                if (filePath != null)
                {
                    string path = Path.Combine("upload_attachments", filePath);

                    var memory = new MemoryStream();
                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    // Add the download count
                    _attachmentService.IncrementDownloadCount(fileName);
                    return File(memory, "application/octet-stream", Path.GetFileName(path));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading file: {ex}");
                return StatusCode(500, new { Message = "Error occurred while downloading file." });
            }
        }

        // Task #4: Preview the file 
        [HttpGet("preview/{fileName}")]
        public async Task<IActionResult> PreviewFile(string fileName)
        {
            try
            {
                string filePath = Path.Combine("upload_attachments", fileName);

                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }

                string fileExtension = Path.GetExtension(fileName).ToLower();
                string contentType = contentType = AttachmentHelper.GetContentType(fileExtension);

                byte[] fileContent = System.IO.File.ReadAllBytes(filePath);

                return File(fileContent, contentType);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error downloading file: {ex}");
                return StatusCode(500, new { Message = "Error occurred while downloading file." });
            }
        }

        //Task #5: Share the attached file and save in the shared Attachment table
        [HttpPost("link/{attachmentId}/{option}")]
        public IActionResult ShareAttachment(int attachmentId, string option)
        {
            try
            {
                int createTime = AttachmentHelper.GetCurrentUnixTimestamp();

                DateTime expireTime;

                switch (option)
                {
                    case "1h":
                        expireTime = DateTime.UtcNow.AddHours(1);
                        break;
                    case "3h":
                        expireTime = DateTime.UtcNow.AddHours(3);
                        break;
                    case "1d":
                        expireTime = DateTime.UtcNow.AddDays(1);
                        break;
                    case "3d":
                        expireTime = DateTime.UtcNow.AddDays(3);
                        break;
                    default:
                        expireTime = DateTime.UtcNow.AddHours(1);
                        break;
                }

                int expireTimeSpan = (int)(expireTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                int sharedId = random.Next(100000, 999999);

                ShareAttachment shareAttachment = new ShareAttachment
                {
                    AttachmentID = attachmentId,
                    SharedID = sharedId,
                    CreateTime = createTime,
                    ExpireTime = expireTimeSpan,
                };

                _shareAttachmentService.AddShareAttachment(shareAttachment);

                string sharedUrl = $"DocLabOrg/{sharedId}";

                return Ok(new { Message = "Attachment shared successfully", SharedUrl = sharedUrl });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while sharing attachment: {ex}");
                return StatusCode(500, new { Message = "Error occurred while sharing attachment" });
            }

        }

        //Task #6: Validate the link of shared file
        [HttpGet("DocLabOrg/{sharedId}")]
        public Task<IActionResult> GetSharedAttachment(int sharedId)
        {

            var sharedAttachment = _shareAttachmentService.GetSharedAttachment(sharedId);
            switch (sharedAttachment)
            {
                case "Expired":
                    return ErrorMessage("The Link has been expired!");


                default:
                    return PreviewFile(sharedAttachment);
            }

        }

        // Task #7 additional delete the attachment  
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteAttachment(int id)
        {
            try
            {
                if (_attachmentService.DeleteAttachment(id))
                {
                    return Ok(new { Message = "Attachment deleted successfully!" });
                }
                else
                {
                    return NotFound(new { Message = "Attachment not found." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting attachment: {ex}");
                return StatusCode(500, new { Message = "Error occurred while deleting attachment." });
            }
        }

        public async Task<IActionResult> ErrorMessage(string message)
        {
            return Content(message);
        }

    }

}

