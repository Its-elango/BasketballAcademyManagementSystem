using BasketballAcademy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BasketballAcademy.Repository
{
    public class CoachRepository : SqlConnectionBase
    {
        /// <summary>
        /// Registers a new coach in the database.
        /// </summary>
        /// <param name="Coach">The coach information to register.</param>
        /// <returns>True if the coach was registered successfully; otherwise, false.</returns>
        public bool RegisterCoach(Coach Coach)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 
                    using (SqlCommand cmd = new SqlCommand("sp_addCoach", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@fullName", Coach.FullName);
                        cmd.Parameters.AddWithValue("@username", Coach.username);
                        cmd.Parameters.AddWithValue("@Email", Coach.Email);
                        cmd.Parameters.AddWithValue("@role", 2);
                        cmd.Parameters.AddWithValue("@Password", Encrypt(Coach.Password));

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
        /// Deletes a coach with the specified ID from the database.
        /// </summary>
        /// <param name="Id">The ID of the coach to delete.</param>
        /// <returns>The number of rows affected by the deletion.</returns>
        public int DeleteCoach(int Id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                
                    using (SqlCommand cmd = new SqlCommand("sp_removeCoach", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", Id); 
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
        /// Registers an event for a coach in the database.
        /// </summary>
        /// <param name="coach">The coach for whom the event is registered.</param>
        /// <returns>True if the event was registered successfully; otherwise, false.</returns>
        public bool RegisterEvent(Coach coach)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_RegisterEvent", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fullName", coach.FullName);
                        cmd.Parameters.AddWithValue("@email", coach.Email);
                        cmd.Parameters.AddWithValue("@username", coach.username);
                        cmd.Parameters.AddWithValue("@photo", coach.photo);
                        cmd.Parameters.AddWithValue("@role", 2);
                        int rowsAffected = cmd.ExecuteNonQuery(); 
                        connection.Close(); 
                        return true;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Updates coach information in the database.
        /// </summary>
        /// <returns>True if the coach information was updated successfully; otherwise, false.</returns>
        public bool UpdateCoach(Coach coach)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open(); 

                    using (SqlCommand cmd = new SqlCommand("sp_updateCoach", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                    
                        cmd.Parameters.AddWithValue("@Id", coach.Id);
                        cmd.Parameters.AddWithValue("@FullName", coach.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", coach.DateOfBirth);
                        cmd.Parameters.AddWithValue("@age", coach.Age);
                        cmd.Parameters.AddWithValue("@Gender", coach.Gender);
                        cmd.Parameters.AddWithValue("@Address", coach.Address);
                        cmd.Parameters.AddWithValue("@PrimarySkill", coach.PrimarySkill);
                        cmd.Parameters.AddWithValue("@Phone", coach.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", coach.Email);
                        cmd.Parameters.AddWithValue("@Experience", coach.Experience);
                        cmd.Parameters.AddWithValue("@photo", coach.photo);
                        cmd.Parameters.AddWithValue("@idproof", coach.idproof);
                        cmd.Parameters.AddWithValue("@CertificateProof", coach.CertificateProof);

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
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of coach objects.</returns>
        public List<Coach> ViewCoach()
        {
            List<Coach> Coachlist = new List<Coach>();
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open(); 

                    SqlCommand cmd = new SqlCommand("sp_viewCoach", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@role", 2);

                    SqlDataAdapter sd = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sd.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Coach coach = new Coach
                        {
                            Id = Convert.ToInt32(dr["id"]),
                            FullName = Convert.ToString(dr["fullName"]),
                            Email = Convert.ToString(dr["email"]),
                            username = Convert.ToString(dr["username"]),
                            DateOfBirth = dr.Field<DateTime>("dateOfBirth"),
                            Age = Convert.ToInt32(dr["age"]),
                            Gender = Convert.ToString(dr["gender"]),
                            PhoneNumber = Convert.ToString(dr["phone"]),
                            Experience = Convert.ToInt32(dr["experience"]),
                            photo = dr.Field<byte[]>("photo"),
                            idproof = dr.Field<byte[]>("idproof"),
                            CertificateProof = dr.Field<byte[]>("CertificateProof")
                        };

                        Coachlist.Add(coach);
                    }
                }           
                finally
                {
                    connection.Close(); 
                }
            }
            return Coachlist;
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
