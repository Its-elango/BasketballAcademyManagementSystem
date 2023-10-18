using BasketballAcademy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Media;
using System.Threading;

namespace BasketballAcademy.Repository
{
    public class AdmissionRepository : SqlConnectionBase
    {
        /// <summary>
        /// Saves admission information to the database.
        /// </summary>
        /// <param name="admission">The Admission object containing admission information.</param>
        /// <returns>True if the admission information was successfully saved; otherwise, false.</returns>

        public int AdmissionForm(Admission admission)
        {
            try {
                using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand cmdCheckUser = new SqlCommand("sp_CheckExisting", connection))
                    {
                        cmdCheckUser.CommandType = CommandType.StoredProcedure;
                        cmdCheckUser.Parameters.AddWithValue("@Email", admission.Email);
                        int userCount = (int)cmdCheckUser.ExecuteScalar();

                        if (userCount > 0)
                        {
                            return -1;
                        }
                    }

                    using (SqlCommand cmd1 = new SqlCommand("sp_CoachAvailability", connection))
                    {
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@CoachID", admission.CoachID);
                        int count = (int)cmd1.ExecuteScalar();

                        if (count > 4)
                        {
                            return -2; 
                        }

                        using (SqlCommand cmd = new SqlCommand("sp_EnrollPlayer", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ID", admission.Id);
                            cmd.Parameters.AddWithValue("@CoachID", admission.CoachID);
                            cmd.Parameters.AddWithValue("@Photo", admission.photo);
                            cmd.Parameters.AddWithValue("@FullName", admission.FullName);
                            cmd.Parameters.AddWithValue("@DateOfBirth", admission.DateOfBirth);
                            cmd.Parameters.AddWithValue("@Age", admission.Age);
                            cmd.Parameters.AddWithValue("@Gender", admission.Gender);
                            cmd.Parameters.AddWithValue("@Phone", admission.PhoneNumber);
                            cmd.Parameters.AddWithValue("@Email", admission.Email);
                            cmd.Parameters.AddWithValue("@ChooseMonth", admission.ChooseMonths);
                            cmd.Parameters.AddWithValue("@ChooseCoach", admission.Coach);
                            cmd.Parameters.AddWithValue("@ParentGuardianName", admission.ParentGuardianName);
                            cmd.Parameters.AddWithValue("@ParentGuardianPhone", admission.ParentGuardianPhone);
                            cmd.Parameters.AddWithValue("@Payment", admission.Payment);
                            cmd.Parameters.AddWithValue("@Status", 0); 

                            int rowsAffected = cmd.ExecuteNonQuery(); 

                            if(rowsAffected > 0)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }
            finally
                {
                Connection.Close();
            }
        }


        /// <summary>
        /// Updates the profile information of a player in the database.
        /// </summary>
        /// <param name="admission">The Admission object containing updated profile information.</param>
        /// <returns>True if the profile information was successfully updated; otherwise, false.</returns>
        public bool UpdateProfile(Admission admission)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_updatePlayer", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", admission.Id);
                        cmd.Parameters.AddWithValue("@FullName", admission.FullName);
                        cmd.Parameters.AddWithValue("@DateOfBirth", admission.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Age", admission.Age);
                        cmd.Parameters.AddWithValue("@Gender", admission.Gender);
                        cmd.Parameters.AddWithValue("@Phone", admission.PhoneNumber);
                        cmd.Parameters.AddWithValue("@Email", admission.Email);
                        cmd.Parameters.AddWithValue("@ParentGuardianName", admission.ParentGuardianName);
                        cmd.Parameters.AddWithValue("@ParentGuardianPhone", admission.ParentGuardianPhone);
                        cmd.Parameters.AddWithValue("@Photo", admission.photo);

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
        /// Retrieves a list of player admission records from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player admission records.</returns>
        public List<Admission> ViewPlayer()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_viewPlayers", connection))
                    {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        connection.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            playerlist.Add(
                                new Admission
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    FullName = Convert.ToString(dr["FullName"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    DateOfBirth = (DateTime)dr["DateOfBirth"],
                                    Age = Convert.ToInt32(dr["Age"]),
                                    Gender = Convert.ToString(dr["Gender"]),
                                    PhoneNumber = Convert.ToString(dr["Phone"]),
                                    ChooseMonths = Convert.ToString(dr["ChooseMonth"]),
                                    Coach = Convert.ToString(dr["ChooseCoach"]),
                                    ParentGuardianName = Convert.ToString(dr["ParentGuardianName"]),
                                    ParentGuardianPhone = Convert.ToString(dr["ParentGuardianPhone"]),
                                    Payment = Convert.ToString(dr["Payment"]),
                                    status = Convert.ToInt32(dr["Status"])
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
        }


        /// <summary>
        /// Retrieves a list of player profiles from the database.
        /// </summary>
        /// <returns>A list of Admission objects representing player profiles.</returns>
        public List<Admission> ViewEnrolledPlayer()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_viewEnrolledPlayer", connection))
                    {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Status",1);
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        connection.Close(); 

                        foreach (DataRow dr in dt.Rows)
                        {
                            playerlist.Add(
                                new Admission
                                {
                                    Id = Convert.ToInt32(dr["ID"]),
                                    FullName = Convert.ToString(dr["FullName"]),
                                    Email = Convert.ToString(dr["Email"]),
                                    DateOfBirth = (DateTime)dr["DateOfBirth"],
                                    Age = Convert.ToInt32(dr["Age"]),
                                    Gender = Convert.ToString(dr["Gender"]),
                                    PhoneNumber = Convert.ToString(dr["Phone"]),
                                    ChooseMonths = Convert.ToString(dr["ChooseMonth"]),
                                    Coach = Convert.ToString(dr["ChooseCoach"]),
                                    ParentGuardianName = Convert.ToString(dr["ParentGuardianName"]),
                                    ParentGuardianPhone = Convert.ToString(dr["ParentGuardianPhone"]),
                                    Payment = Convert.ToString(dr["Payment"]),
                                    status = Convert.ToInt32(dr["Status"]),
                                 
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
        }

        /// <summary>
        /// Updates the status of a player in the database.
        /// </summary>
        /// <param name="id">The ID of the player to update.</param>
        /// <param name="status">The new status to set for the player.</param>
        /// <returns>True if the status was successfully updated; otherwise, false.</returns>
        public bool UpdateState(int id, int status)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_ChangeStatus", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.ExecuteNonQuery();

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
        /// Deletes a player from the database by their ID.
        /// </summary>
        /// <param name="Id">The ID of the player to delete.</param>
        /// <returns>The number of rows affected by the deletion (usually 1 if successful).</returns>
        public int DeletePlayer(int Id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_removePlayer", connection))
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
        /// Retrieves a list of coaches from the database.
        /// </summary>
        /// <returns>A list of Coach objects representing coaches.</returns>
        public List<Coach> CoachList()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_CoachList", connection))
                    {
                        List<Coach> coachlist = new List<Coach>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("role", 2);

                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);

                        connection.Close(); 

                        foreach (DataRow dr in dt.Rows)
                        {
                            coachlist.Add(
                                new Coach
                                {
                                    Id = Convert.ToInt32(dr["id"]),
                                    FullName = Convert.ToString(dr["fullName"]),
                                    photo = dr.Field<byte[]>("photo"),
                                    Experience = Convert.ToInt32(dr["Experience"]),
                                    PrimarySkill = Convert.ToString(dr["PrimarySkill"]),
                                   
                                });
                        }

                        return coachlist;
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }
        }



        /// <summary>
        /// Retrieves a list of players by coach name from the database.
        /// </summary>
        /// <param name="name">The name of the coach whose players are to be retrieved.</param>
        /// <returns>A list of Admission objects representing players.</returns>
        public List<Admission> PlayerList(string name)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_ListPlayers", connection))
                    {
                        List<Admission> playerlist = new List<Admission>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ChooseCoach", name);

                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);

                        connection.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            playerlist.Add(
                                new Admission
                                {
                                    FullName = Convert.ToString(dr["FullName"]),
                                    photo = dr.Field<byte[]>("Photo"),
                                    Gender = Convert.ToString(dr["Gender"]),
                                    Age = Convert.ToInt32(dr["Age"]),
                                    
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
        }


        int CoachID;
        public List<Events> GetEventsByPlayer(int Id)
        {
            List<Events> eventlist = new List<Events>(); 

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    connection.Open(); 

                    using (SqlCommand cmd = new SqlCommand("sp_GetCoachIdByPlayerID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", Id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CoachID = Convert.ToInt32(reader["CoachID"]);
                            }
                        }
                    }
                    using (SqlCommand comd = new SqlCommand("sp_GetEventIdByCoachID", connection))
                    {
                        comd.CommandType = CommandType.Text;
                        comd.Parameters.AddWithValue("@CoachID", CoachID);

                        List<int> eventIds = new List<int>(); 

                        using (SqlDataReader reader = comd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int eventId = Convert.ToInt32(reader["EventID"]);
                                eventIds.Add(eventId);
                            }
                        }
                        using (SqlCommand eventCmd = new SqlCommand("sp_GetEventByEventID", connection))
                        {
                            eventCmd.CommandType = CommandType.StoredProcedure;

                            foreach (int eventId in eventIds)
                            {
                                eventCmd.Parameters.Clear(); 
                                eventCmd.Parameters.AddWithValue("@EventID", eventId);

                                using (SqlDataReader reader = eventCmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        eventlist.Add(new Events
                                        {
                                            EventID = Convert.ToInt32(reader["EventID"]),
                                            EventName = Convert.ToString(reader["EventName"]),
                                            EventDate = (DateTime)reader["EventDate"],
                                            EventTime = (TimeSpan)reader["EventTime"],
                                            Incharge = Convert.ToString(reader["Incharge"]),
                                            Venue = Convert.ToString(reader["Venue"]),
                                            Details = Convert.ToString(reader["Details"]),
                                            AgeGroup = Convert.ToString(reader["AgeGroup"]),
                                            EventImage = (byte[])reader["EventImage"],
                                            PrizeDetails = Convert.ToString(reader["PrizeDetails"]),
                                            Contact = Convert.ToString(reader["Contact"]),
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }

                return eventlist;
            }
        }

    }
}