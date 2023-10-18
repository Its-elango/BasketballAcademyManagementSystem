using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BasketballAcademy.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class UserRepository : SqlConnectionBase
    {
        /// <summary>
        /// Registers a new user in the database.
        /// </summary>
        /// <param name="user">The user information to register.</param>
        /// <returns>True if the user was registered successfully; otherwise, false.</returns>
        public bool RegisterUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open();

                 
                    using (SqlCommand checkCmd = new SqlCommand("sp_CheckExistingByUsername", connection))
                    {
                        checkCmd.CommandType = CommandType.StoredProcedure;
                        checkCmd.Parameters.AddWithValue("@username", user.username);

                        int userCount = (int)checkCmd.ExecuteScalar();

                        if (userCount > 0)
                        {
                         
                            return false;
                        }
                    }

          
                    using (SqlCommand cmd = new SqlCommand("sp_insertUser", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fullName", user.FullName);
                        cmd.Parameters.AddWithValue("@username", user.username);
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@role", 3);
                        cmd.Parameters.AddWithValue("@password", Encrypt(user.Password));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected >= 1;
                    }
                }
                finally
                {
                    connection.Close(); 
                }
            }
        }



        /// <summary>
        /// Deletes a user with the specified ID from the database.
        /// </summary>
        /// <param name="ID">The ID of the user to delete.</param>
        /// <returns>The number of rows affected by the deletion.</returns>
        public int DeleteUser(int ID)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 
                    using (SqlCommand cmd = new SqlCommand("sp_removeUser", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", ID);
                        int i = cmd.ExecuteNonQuery(); 
                        connection.Close(); 
                        return i; 
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Retrieves a list of users with a specific role from the database.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        public List<User> ViewUser()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    List<User> playerlist = new List<User>();

                    SqlCommand cmd = new SqlCommand("sp_viewUser", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@role", 3);

                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        playerlist.Add(
                            new User
                            {
                                ID = Convert.ToInt32(dr["ID"]),
                                FullName = Convert.ToString(dr["FullName"]),
                                username = Convert.ToString(dr["username"]),
                                Email = Convert.ToString(dr["Email"]),
                            });
                    }
                    return playerlist;
                }
            }
            finally
            {
                CloseConnection();
            }
        }

        /// <summary>
        /// Encrypts a plaintext string using AES encryption with a predefined encryption key.
        /// </summary>
        /// <param name="plaintext">The plaintext string to be encrypted.</param>
        /// <returns>The encrypted string in base64 format.</returns>

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
