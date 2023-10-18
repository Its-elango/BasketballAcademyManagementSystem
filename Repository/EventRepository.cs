using BasketballAcademy.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Configuration;
using System.Drawing;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace BasketballAcademy.Repository
{
    public class EventRepository : SqlConnectionBase
    {
        /// <summary>
        /// Adds an event to the database.
        /// </summary>
        /// <param name="events">The event information to add.</param>
        /// <returns>True if the event was added successfully; otherwise, false.</returns>
        public bool AddEvents(Events events )
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_addEvent", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EventName", events.EventName);
                        cmd.Parameters.AddWithValue("@EventDate", events.EventDate);
                        cmd.Parameters.AddWithValue("@EventTime", events.EventTime);
                        cmd.Parameters.AddWithValue("@Venue", events.Venue);
                        cmd.Parameters.AddWithValue("@Details", events.Details);
                        cmd.Parameters.AddWithValue("@Incharge", events.Incharge);
                        cmd.Parameters.AddWithValue("@AgeGroup", events.AgeGroup);
                        cmd.Parameters.AddWithValue("@Contact", events.Contact);
                        cmd.Parameters.AddWithValue("@EventImage", events.EventImage);
                        cmd.Parameters.AddWithValue("@PrizeDetails", events.PrizeDetails);

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
        /// Deletes an event with the specified ID from the database.
        /// </summary>
        /// <param name="Id">The ID of the event to delete.</param>
        /// <returns>The number of rows affected by the deletion, or -1 if an error occurred.</returns>
        public int DeleteEvent(int Id)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection(); 

                    using (SqlCommand cmd = new SqlCommand("sp_removeEvent", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", Id);
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
        /// Retrieves a list of events from the database.
        /// </summary>
        /// <returns>A list of event objects.</returns>
        public List<Events> ViewEvents()
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_viewEvent", connection))
                    {
                        List<Events> eventlist = new List<Events>();
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sd = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sd.Fill(dt);
                        connection.Close();

                        foreach (DataRow dr in dt.Rows)
                        {
                            eventlist.Add(
                                new Events
                                {
                                    EventID = Convert.ToInt32(dr["EventID"]),
                                    EventName = Convert.ToString(dr["EventName"]),
                                    EventDate = (DateTime)dr["EventDate"],
                                    EventTime = ((TimeSpan)dr["EventTime"]),
                                    Incharge = Convert.ToString(dr["Incharge"]),
                                    Venue = Convert.ToString(dr["Venue"]),
                                    Details = Convert.ToString(dr["Details"]),
                                    AgeGroup = Convert.ToString(dr["AgeGroup"]),
                                    EventImage = (byte[])dr["EventImage"],
                                    PrizeDetails = Convert.ToString(dr["PrizeDetails"]),
                                    Contact = Convert.ToString(dr["Contact"]),
                                });
                        }
                        return eventlist;
                    }
                }

                finally
                {
                    CloseConnection();
                }
            }
        }

        /// <summary>
        /// Retrieves a list of registrations for a specific event.
        /// </summary>
        /// <param name="id">The ID of the event for which registrations are to be retrieved.</param>
        /// <returns>A list of Registeration objects representing event registrations.</returns>
        public List<Registeration> GetRegistrationData(int id)
        {
            List<Registeration> registerlist = new List<Registeration>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_ViewRegistration", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", id);
                        connection.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                registerlist.Add(new Registeration
                                {
                                    Name = Convert.ToString(reader["fullName"])
                                });
                            }
                        }
                    }
                }
                finally
                {
                    CloseConnection();
                }
            }

            return registerlist;
        }

        /// <summary>
        /// Registers a coach for an event, ensuring no duplicate registrations.
        /// </summary>
        /// <param name="eventId">The ID of the event to register for.</param>
        /// <param name="coachId">The ID of the coach to register.</param>
        /// <returns>True if the coach was successfully registered for the event; otherwise, false.</returns>
        public bool RegisterEvent(int eventId, int coachId)
        {
            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();
                    using (SqlCommand cmd = new SqlCommand("sp_EventCheckRegistration", connection))
                    {
                        connection.Open();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EventID", eventId);
                        cmd.Parameters.AddWithValue("@CoachID", coachId);

                        int existingRegistrations = (int)cmd.ExecuteScalar();

                        if (existingRegistrations > 0)
                        {
                            return false; 
                        }
                        cmd.CommandText = "sp_EventReg";
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
        /// Retrieves a list of events associated with a specific coach.
        /// </summary>
        /// <param name="coachId">The ID of the coach for whom events are to be retrieved.</param>
        /// <returns>A list of Events objects representing events associated with the coach.</returns>
        public List<Events> GetEventsByCoachId(int coachId)
        {
            List<Events> eventlist = new List<Events>();

            using (SqlConnection connection = new SqlConnection(Connection.ConnectionString))
            {
                try
                {
                    OpenConnection();

                    using (SqlCommand cmd = new SqlCommand("sp_GetEventIdByCoachID", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CoachID", coachId);

                        connection.Open();

                        List<int> eventIds = new List<int>();

                        using (SqlDataReader reader = cmd.ExecuteReader())
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
                    CloseConnection();
                }

                return eventlist;
            }
        }

    }

}