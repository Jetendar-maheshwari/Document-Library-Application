using Document_library_application.Models;
using MySql.Data.MySqlClient;

namespace Document_library_application.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<AttachmentRepository> _logger;

        public AttachmentRepository(string connectionString, ILogger<AttachmentRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        //Get list of attachment form database table attachment
        public List<Attachment> GetAllAttachments()
        {
            List<Attachment> attachments = new List<Attachment>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM attachment";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Attachment attachment = new Attachment
                                {
                                    ID = reader.GetInt32("ID"),
                                    FileName = reader.IsDBNull(reader.GetOrdinal("fileName")) ? null : reader.GetString("fileName"),
                                    CreateTime = reader.IsDBNull(reader.GetOrdinal("createTime")) ? 0 : reader.GetInt32("createTime"),
                                    FilePath = reader.IsDBNull(reader.GetOrdinal("filePath")) ? null : reader.GetString("filePath"),
                                    MimeType = reader.IsDBNull(reader.GetOrdinal("mimeType")) ? null : reader.GetString("mimeType"),
                                    DownloadCount = reader.IsDBNull(reader.GetOrdinal("downloadCount")) ? 0 : reader.GetInt32("downloadCount"),
                                    Role = reader.IsDBNull(reader.GetOrdinal("role")) ? null : reader.GetString("role"),
                                    Meta = reader.IsDBNull(reader.GetOrdinal("meta")) ? null : reader.GetString("meta")
                                };

                                attachments.Add(attachment);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while fetching attachments: {ex}");
            }

            return attachments;
        }

        // Add the attachmend inside the attachment table
        public void AddAttachment(Attachment attachment)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO attachment (fileName, filePath, createTime, downloadCount, mimeType) VALUES (@FileName, @FilePath, @CreateTime, @DownloadCount, @MimeType)";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FileName", attachment.FileName);
                        cmd.Parameters.AddWithValue("@FilePath", attachment.FilePath);
                        cmd.Parameters.AddWithValue("@CreateTime", attachment.CreateTime);
                        cmd.Parameters.AddWithValue("@DownloadCount", attachment.DownloadCount);
                        cmd.Parameters.AddWithValue("@MimeType", attachment.MimeType);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding attachment: {ex}");
            }
        }

        //Delete the attachment form attachment table
        public bool DeleteAttachment(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM attachment WHERE ID = @ID";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting attachment: {ex}");
                return false;
            }
        }

        // Get the file path from database table attachment
        public async Task<string> GetFilePath(string fileName)
        {
            string filePath = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string query = "SELECT FilePath FROM attachment WHERE FileName = @FileName";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        object result = await cmd.ExecuteScalarAsync();
                        filePath = result?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting file path: {ex}");
            }
            return filePath;
        }

        //Add the attachment download count in the database table attachment
        public void IncrementDownloadCount(string fileName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE attachment SET DownloadCount = DownloadCount + 1 WHERE FileName = @FileName";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while incrementing download count: {ex}");
            }
        }

    }
}
