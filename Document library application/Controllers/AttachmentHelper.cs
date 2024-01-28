using System;
using Microsoft.AspNetCore.StaticFiles;

namespace Document_library_application.Helpers
{
    public static class AttachmentHelper
    {
        //Convert the time span in the unix timestamp
        public static int GetCurrentUnixTimestamp()
        {
            DateTime currentTime = DateTime.UtcNow;
            int unixTimestamp = (int)(currentTime - new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        //get the attachment mimeType
        public static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out string mimeType))
            {
                mimeType = "application/octet-stream";
            }

            return mimeType;
        }

        //Based on the file extension return content/Type
        public static string GetContentType(string extension)
        {
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                default:
                    return "application/octet-stream";
            }
        }
    }
}
