using Document_library_application.Helpers;
using Document_library_application.Models;
using Document_library_application.Repositories;
using MySql.Data.MySqlClient;
using System;

namespace Document_library_application.Repository
{
    public class ShareAttachmnetRepository : IShareAttachmentRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<ShareAttachmnetRepository> _logger;

        public ShareAttachmnetRepository(string connectionString, ILogger<ShareAttachmnetRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public void AddShareAttachment(ShareAttachment shareAttachment)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO shared_attachment (attachmentID, sharedID, createTime, expireTime) VALUES (@AttachmentID, @SharedID, @CreateTime, @ExpireTime)";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AttachmentID", shareAttachment.AttachmentID);
                        command.Parameters.AddWithValue("@SharedID", shareAttachment.SharedID);
                        command.Parameters.AddWithValue("@CreateTime", shareAttachment.CreateTime);
                        command.Parameters.AddWithValue("@ExpireTime", shareAttachment.ExpireTime);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected == 0)
                        {
                            _logger.LogWarning("No rows were affected while adding attachment.");
                        }
                        else
                        {
                            _logger.LogInformation("Attachment added successfully.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                _logger.LogError($"MySqlException occurred while adding attachment: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while adding attachment: {ex.Message}");
            }
        }


        public string GetSharedAttachment(int sharedID)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT a.filePath, sa.expireTime
                             FROM shared_attachment sa
                             INNER JOIN attachment a ON sa.attachmentID = a.ID
                             WHERE sa.sharedID = @SharedID";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@SharedID", sharedID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int expireTime = reader.GetInt32("expireTime");
                                if (expireTime < AttachmentHelper.GetCurrentUnixTimestamp())
                                {
                                    // Attachment path has expired
                                    return "Expired";
                                }

                                return reader.GetString("filePath");
                                                               
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while retrieving shared attachment: {ex}");
            }


            return "";
        }


    }
}
