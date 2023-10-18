using BasketballAcademy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class AdminRepository : SqlConnectionBase
    {
        /// <summary>
        /// Adds a new Admin to the database.
        /// </summary>
        /// <param name="admin">The Admin object containing admin information.</param>
        /// <returns>True if the admin was successfully added; otherwise, false.</returns>
        public bool AddAdmin(Admin admin)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_addAdmin", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        cmd.Parameters.AddWithValue("@fullName", admin.fullName);
                        cmd.Parameters.AddWithValue("@username", admin.username);
                        cmd.Parameters.AddWithValue("@email", admin.email);
                        cmd.Parameters.AddWithValue("@role", 0); 
                        cmd.Parameters.AddWithValue("@password", Encrypt( admin.password));

                        int rowsAffected = cmd.ExecuteNonQuery(); 
                        connection.Close(); 
                
                        return rowsAffected >= 1;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Deletes an Admin from the database by their ID.
        /// </summary>
        /// <param name="id">The ID of the Admin to be deleted.</param>
        /// <returns>The number of rows affected (should be 1 if successful, 0 if no rows were deleted).</returns>
        public int DeleteAdmin(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_removeAdmin", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();
                 
                        return rowsAffected;
                    }
                }
                finally
                {
                    CloseConnection(); 
                }
            }
        }

        /// <summary>
        /// Retrieves a list of Admins with a specific role from the database.
        /// </summary>
        /// <returns>A list of Admin objects with the specified role.</returns>
        public List<Admin> ViewAdmin()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_viewAdmin", connection))
                    {
                        List<Admin> adminlist = new List<Admin>();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@role", 0);

                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        connection.Close(); 

                        foreach (DataRow dr in dt.Rows)
                        {
                            adminlist.Add(
                                new Admin
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    fullName = Convert.ToString(dr["fullName"]),
                                    username = Convert.ToString(dr["username"]),
                                    email = Convert.ToString(dr["email"]),
                                  
                                });
                        }

                        return adminlist;
                    }
                }
                finally
                {
                    CloseConnection(); 
                }
            }
        }


        /// <summary>
        /// Inserts a message into the database.
        /// </summary>
        /// <param name="contact">The contact information to be saved.</param>
        /// <returns>True if the message was successfully saved; otherwise, false.</returns>
        public bool Message(Contact contact)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_EnterMessage", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Name", contact.Name);
                        cmd.Parameters.AddWithValue("@Email", contact.Email);
                        cmd.Parameters.AddWithValue("@Phone", contact.Phone);
                        cmd.Parameters.AddWithValue("@Message", contact.Message);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();
                        return rowsAffected >= 1;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Retrieves a list of messages from the database.
        /// </summary>
        /// <returns>A list of Contact objects representing the messages.</returns>
        public List<Contact> ViewMessage()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_ViewMessage", connection))
                    {
                        List<Contact> message = new List<Contact>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        connection.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            message.Add(
                                new Contact
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    Phone = Convert.ToString(dr["Phone"]),
                                    Message = Convert.ToString(dr["Message"]),
                                });
                        }
                        return message;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Deletes a message from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the message to be deleted.</param>
        /// <returns>The number of rows affected by the deletion operation.</returns>
        public int DeleteMessage(int id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_DeleteMessage", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        connection.Close();
                        return rowsAffected;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Encrypts a plaintext string using AES encryption.
        /// </summary>
        /// <param name="plaintext">The plaintext string to be encrypted.</param>
        /// <returns>The encrypted string.</returns>
        private string Encrypt(string plaintext)
        {
            string encryptionKey = "MAKV2SPBNI99212";

            byte[] clearBytes = Encoding.Unicode.GetBytes(plaintext);

            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(encryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });

                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    plaintext = Convert.ToBase64String(ms.ToArray());
                }
            }
            return plaintext;
        }

    }
}