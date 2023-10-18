using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using BasketballAcademy.Models;
using System.Drawing;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Net;

namespace BasketballAcademy.Repository
{
    public class CredentialsRepository : SqlConnectionBase
    {
        /// <summary>
        /// Authenticates a user by validating their credentials against the database.
        /// </summary>
        /// <param name="credentials">The user's credentials (username and password).</param>
        /// <param name="result">An output parameter indicating the user's role.</param>
        /// <param name="id">An output parameter indicating the user's ID.</param>
        /// <param name="name">An output parameter indicating the user's full name.</param>
        ///  <param name="email">An output parameter indicating the user's email.</param>
        /// <returns>True if authentication is successful; otherwise, false.</returns>
        public bool SignIn(Credentials credentials, out int result, out int id, out string name, out string email)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_ValidateUser", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", credentials.Username);
                        cmd.Parameters.AddWithValue("@password", Encrypt(credentials.Password));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                result = (int)reader["role"];
                                id = (int)reader["ID"];
                                name = (string)reader["fullName"];
                                email = (string)reader["email"];

                                HttpContext.Current.Session["Id"] = id;
                                HttpContext.Current.Session["fullName"] = name;
                                HttpContext.Current.Session["email"] = email;
                            
                                FormsAuthentication.SetAuthCookie(credentials.Username, true);

                                return true;
                            }
                            else
                            {
                                result = 5;
                                id = 5;
                                name = "!!!!";
                                email = "###";
                                return false;
                            }
                        }
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }
        /// <summary>
        /// Handles the password reset process for a user.
        /// </summary>
        /// <param name="credentials">The user's credentials, including the username and email for password reset.</param>
        /// <returns>True if the password was successfully reset; otherwise, false.</returns>
        public bool Forgot(Credentials credentials)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_ForgotPassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;                     
                        cmd.Parameters.AddWithValue("@username", credentials.Username);
                        cmd.Parameters.AddWithValue("@password", Encrypt(credentials.Password));
                        cmd.Parameters.AddWithValue("@email", credentials.Email);

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
        /// Encrypts a plaintext string using AES encryption.
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